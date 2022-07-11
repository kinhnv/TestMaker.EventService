using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TestMaker.Common.Models;
using TestMaker.EventService.Domain.Models;
using TestMaker.EventService.Domain.Models.Candidate;
using TestMaker.EventService.Domain.Services;
using TestMaker.EventService.Infrastructure.Entities;
using TestMaker.EventService.Infrastructure.MongoEntities;
using TestMaker.EventService.Infrastructure.Repositories.CandidateAnswers;
using TestMaker.EventService.Infrastructure.Repositories.Candidates;

namespace TestMaker.EventService.Infrastructure.Services
{
    public class CandidatesService : ICandidatesService
    {
        private readonly ICandidatesRepository _candidatesRepository;
        private readonly ICandidateAnswersRepository _candidateAnswersRepository;
        private readonly ICandidatePreparedTestTempsRepository _candidatePreparedTestTempsRepository;
        private readonly IMapper _mapper;

        public CandidatesService(
            ICandidatesRepository candidatesRepository,
            ICandidateAnswersRepository candidateAnswersRepository,
            IMapper mapper,
            ICandidatePreparedTestTempsRepository candidatePreparedTestTempsRepository)
        {
            _candidatesRepository = candidatesRepository;
            _candidateAnswersRepository = candidateAnswersRepository;
            _mapper = mapper;
            _candidatePreparedTestTempsRepository = candidatePreparedTestTempsRepository;
        }

        public async Task<ServiceResult<CandidateForDetails>> CreateCandidateAsync(CandidateForCreating candidate)
        {
            var entity = _mapper.Map<Candidate>(candidate);
            entity.Code = CreateCode(8);
            entity.IsDeleted = false;
            entity.CreatedAt = DateTime.UtcNow.AddHours(7);
            entity.Status = (int)CandidateStatus.Open;

            await _candidatesRepository.CreateAsync(entity);

            return await GetCandidateAsync(entity.CandidateId);
        }

        public async Task<ServiceResult> DeleteCandidateAsync(Guid candidateId)
        {
            var candidate = await _candidatesRepository.GetAsync(candidateId);
            if (candidate == null || candidate.IsDeleted == true)
            {
                return new ServiceNotFoundResult<Candidate>(candidateId);
            }
            candidate.IsDeleted = true;
            await EditCandidateAsync(_mapper.Map<CandidateForEditing>(candidate));
            return new ServiceResult();
        }

        public async Task<ServiceResult<CandidateForDetails>> EditCandidateAsync(CandidateForEditing candidate)
        {
            var entity = _mapper.Map<Candidate>(candidate);

            var result = await _candidatesRepository.GetAsync(candidate.CandidateId);
            if (result == null || result.IsDeleted == true)
            {
                return new ServiceNotFoundResult<CandidateForDetails>(candidate.CandidateId);
            }

            await _candidatesRepository.UpdateAsync(entity);
            return await GetCandidateAsync(entity.CandidateId);
        }

        public async Task<ServiceResult<CandidateForDetails>> GetCandidateAsync(Guid candidateId)
        {
            var question = await _candidatesRepository.GetAsync(candidateId);

            if (question == null)
                return new ServiceNotFoundResult<CandidateForDetails>(candidateId);

            return await Task.FromResult(new ServiceResult<CandidateForDetails>(_mapper.Map<CandidateForDetails>(question)));
        }

        public async Task<ServiceResult<GetPaginationResult<CandidateForList>>> GetCandidatesAsync(GetCandidatesParams getCandidatesParams)
        {
            Expression<Func<Candidate, bool>> predicate = x => x.IsDeleted == getCandidatesParams.IsDeleted &&
                (getCandidatesParams.EventId == null || getCandidatesParams.EventId == x.EventId);

            var quetsions = (await _candidatesRepository.GetAsync(predicate, getCandidatesParams.Skip, getCandidatesParams.Take))
                .Select(section => _mapper.Map<CandidateForList>(section));
            var count = await _candidatesRepository.CountAsync(predicate);
            var result = new GetPaginationResult<CandidateForList>
            {
                Data = quetsions.ToList(),
                Page = getCandidatesParams.Page,
                Take = getCandidatesParams.Take,
                TotalPage = count
            };

            return new ServiceResult<GetPaginationResult<CandidateForList>>(result);
        }

        private string CreateCode(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[new Random().Next(s.Length)]).ToArray());
        }

        public async Task<ServiceResult<string>> GetAnswerAsync(Guid candidateId, Guid questionId)
        {
            var result = await _candidateAnswersRepository.GetCandidateAnswerByCandidateIdAndQuestionIdAsync(candidateId, questionId);

            return new ServiceResult<string>() { Data = result?.AnswerAsJson ?? String.Empty };
        }

        public async Task<ServiceResult<List<TestMaker.EventService.Domain.Models.CandidateAnswer>>> GetAnswersAsync(Guid candidateId)
        {
            var data = await _candidateAnswersRepository.GetCandidateAnswersByCandidateIdAsync(candidateId);

            if (data != null)
            {
                return new ServiceResult<List<TestMaker.EventService.Domain.Models.CandidateAnswer>>(data.Select(ca => new TestMaker.EventService.Domain.Models.CandidateAnswer
                {
                    AnswerAsJson = ca.AnswerAsJson,
                    QuestionId = ca.QuestionId,
                }).ToList());
            }

            return new ServiceResult<List<TestMaker.EventService.Domain.Models.CandidateAnswer>>(new List<Domain.Models.CandidateAnswer>());
        }

        public async Task<ServiceResult> SubmitQuestionAsync(CandidateAnswerForSubmit answer)
        {
            var candidateAnswer = await _candidateAnswersRepository.GetCandidateAnswerByCandidateIdAndQuestionIdAsync(answer.CandidateId, answer.QuestionId);

            if (candidateAnswer == null)
            {
                await _candidateAnswersRepository.CreateAsync(new Entities.CandidateAnswer
                {
                    CandidateId = answer.CandidateId,
                    QuestionId = answer.QuestionId,
                    AnswerAsJson = answer.AnswerAsJson
                });
            }
            else
            {
                candidateAnswer.AnswerAsJson = answer.AnswerAsJson;
                await _candidateAnswersRepository.UpdateAsync(candidateAnswer);
            }
            return new ServiceResult();
        }

        public async Task<ServiceResult> SubmitCandidateAsync(Guid candidateId)
        {
            var candidate = await _candidatesRepository.GetAsync(candidateId);
            if (candidate != null)
            {
                candidate.Status = (int)CandidateStatus.Done;
                await _candidatesRepository.UpdateAsync(candidate);
            }
            return new ServiceResult();
        }
        public async Task<ServiceResult> ClearAnswersOfCandidateAsync(Guid candidateId)
        {
            await _candidateAnswersRepository.DeleteCandidateAnswersByCandidateIdAsync(candidateId);
            return new ServiceResult();
        }

        public async Task<ServiceResult> CreatePreparedTestTempAsync(Guid candidateId, PreparedTest preparedTest)
        {
            await _candidatePreparedTestTempsRepository.CreateAsync(new MongoEntities.CandidatePreparedTestTemp
            {
                CandidateId = candidateId,
                PreparedTest = preparedTest
            });

            return new ServiceResult();
        } 

        public async Task<ServiceResult<PreparedTest>> GetPreparedTestTempAsync(Guid candidateId)
        {
            var temps = await _candidatePreparedTestTempsRepository.GetAsync(Builders<CandidatePreparedTestTemp>.Filter.Eq(x => x.CandidateId, candidateId));
            if (!(temps?.Count > 0))
            {
                return new ServiceNotFoundResult<PreparedTest>(candidateId);
            }
            if (temps?.Count > 1)
            {
                return new ServiceResult<PreparedTest>($"There are more than one prepared test with candidateid {candidateId}");
            }
            return new ServiceResult<PreparedTest>(temps.Single().PreparedTest);
        }
    }
}
