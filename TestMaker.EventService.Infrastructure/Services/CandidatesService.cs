using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestMaker.EventService.Domain.Models;
using TestMaker.EventService.Domain.Models.Candidate;
using TestMaker.EventService.Domain.Services;
using TestMaker.EventService.Infrastructure.Entities;
using TestMaker.EventService.Infrastructure.Repositories.CandidateAnswers;
using TestMaker.EventService.Infrastructure.Repositories.Candidates;

namespace TestMaker.EventService.Infrastructure.Services
{
    public class CandidatesService : ICandidatesService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly ICandidatesRepository _candidatesRepository;
        private readonly ICandidateAnswersRepository _candidateAnswersRepository;
        private readonly IMapper _mapper;

        public CandidatesService(
            ApplicationDbContext dbContext,
            ICandidatesRepository candidatesRepository, 
            ICandidateAnswersRepository candidateAnswersRepository, 
            IMapper mapper)
        {
            _dbContext = dbContext;
            _candidatesRepository = candidatesRepository;
            _candidateAnswersRepository = candidateAnswersRepository;
            _mapper = mapper;
        }

        public async Task<CandidateForDetails> CreateCandidateAsync(CandidateForCreating candidate)
        {
            var entity = _mapper.Map<Candidate>(candidate);
            entity.Code = CreateCode(8);
            entity.Status = (int)CandidateStatus.Open;
            _dbContext.Candidates.Add(entity);
            await _dbContext.SaveChangesAsync();

            return await GetCandidateAsync(entity.CandidateId);
        }

        public async Task DeleteCandidateAsync(Guid candidateId)
        {
            var candidate = await _dbContext.Candidates.FindAsync(candidateId);
            if (candidate != null)
            {
                _dbContext.Candidates.Remove(candidate);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task EditCandidateAsync(CandidateForEditing candidate)
        {
            var entity = _mapper.Map<Candidate>(candidate);

            _dbContext.Entry(entity).State = EntityState.Modified;

            await _dbContext.SaveChangesAsync();
        }

        public async Task<CandidateForDetails> GetCandidateAsync(Guid candidateId)
        {
            var candidate = _dbContext.Candidates.SingleOrDefault(x => x.CandidateId == candidateId);

            if (candidate == null)
                return null;

            return await Task.FromResult(_mapper.Map<CandidateForDetails>(candidate));
        }

        public async Task<IEnumerable<CandidateForList>> GetCandidatesAsync(GetCandidateFilter filter)
        {
            var query = _dbContext.Candidates.AsQueryable();
            if (filter != null)
            {
                if (filter.EventId != Guid.Empty)
                {
                    query = query.Where(x => x.EventId == filter.EventId);
                }
            }
            var candidates = query.ToList().Select(e => _mapper.Map<CandidateForList>(e));
            return await Task.FromResult(candidates);
        }

        public async Task<bool> CandidateExistsAsync(Guid candidateId)
        {
            return await _dbContext.Candidates.AnyAsync(e => e.CandidateId == candidateId);
        }

        private string CreateCode(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[new Random().Next(s.Length)]).ToArray());
        }

        public async Task<string> GetAnswerAsync(Guid candidateId, Guid questionId)
        {
            var result = await _candidateAnswersRepository.GetCandidateAnswerByCandidateIdAndQuestionIdAsync(candidateId, questionId);

            return result?.AnswerAsJson ?? String.Empty;
        }

        public async Task<List<TestMaker.EventService.Domain.Models.CandidateAnswer>> GetAnswersAsync(Guid candidateId)
        {
            var data = await _candidateAnswersRepository.GetCandidateAnswersByCandidateIdAsync(candidateId);

            if (data != null)
            {
                return data.Select(ca => new TestMaker.EventService.Domain.Models.CandidateAnswer
                {
                    AnswerAsJson = ca.AnswerAsJson,
                    QuestionId = ca.QuestionId,
                }).ToList();
            }

            return new List<Domain.Models.CandidateAnswer>();
        }

        public async Task SubmitQuestionAsync(CandidateAnswerForSubmit answer)
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
        }

        public async Task SubmitCandidateAsync(Guid candidateId)
        {
            var candidate = await _candidatesRepository.GetAsync(candidateId);
            if (candidate != null)
            {
                candidate.Status = (int)CandidateStatus.Done;
                await _candidatesRepository.UpdateAsync(candidate);
            }
        }
        public async Task ClearAnswersOfCandidateAsync(Guid candidateId)
        {
            await _candidateAnswersRepository.DeleteCandidateAnswersByCandidateIdAsync(candidateId);
        }
    }
}
