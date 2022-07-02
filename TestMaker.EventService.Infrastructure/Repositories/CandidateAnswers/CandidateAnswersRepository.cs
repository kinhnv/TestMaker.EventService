using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestMaker.Common.Repository;
using TestMaker.EventService.Infrastructure.Entities;

namespace TestMaker.EventService.Infrastructure.Repositories.CandidateAnswers
{
    public class CandidateAnswersRepository : Repository<CandidateAnswer>, ICandidateAnswersRepository
    {
        public CandidateAnswersRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<CandidateAnswer> GetCandidateAnswerByCandidateIdAndQuestionIdAsync(Guid candidateId, Guid questionId)
        {
            var candidateAnswer = _dbContext.Set<CandidateAnswer>().FirstOrDefault(ca => ca.CandidateId == candidateId && ca.QuestionId == questionId);

            return await Task.FromResult(candidateAnswer);
        }

        public async Task<List<CandidateAnswer>> GetCandidateAnswersByCandidateIdAsync(Guid candidateId)
        {
            var candidateAnswers = _dbContext.Set<CandidateAnswer>().Where(ca => ca.CandidateId == candidateId).ToList();

            return await Task.FromResult(candidateAnswers);
        }

        public async Task DeleteCandidateAnswersByCandidateIdAsync(Guid candidateId)
        {
            var candidateAnswers = _dbContext.Set<CandidateAnswer>().Where(ca => ca.CandidateId == candidateId).ToList();

            if (candidateAnswers.Any())
            {
                _dbContext.RemoveRange(candidateAnswers);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
