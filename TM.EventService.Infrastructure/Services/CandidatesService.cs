using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestMaker.Business.Admin.Domain.Models.Candidate;
using TestMaker.Business.Admin.Domain.Services;
using TM.EventService.Infrastructure.Entities;

namespace TM.EventService.Infrastructure.Services
{
    public class CandidatesService : ICandidatesService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public CandidatesService(ApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
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
    }
}
