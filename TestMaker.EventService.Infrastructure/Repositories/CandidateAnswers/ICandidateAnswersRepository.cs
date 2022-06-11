using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestMaker.EventService.Infrastructure.Entities;
using TestMaker.EventService.Infrastructure.Repository;

namespace TestMaker.EventService.Infrastructure.Repositories.CandidateAnswers
{
    public interface ICandidateAnswersRepository: IRepository<CandidateAnswer>
    {
        Task<CandidateAnswer> GetCandidateAnswerByCandidateIdAndQuestionIdAsync(Guid candidateId, Guid questionId);

        Task<List<CandidateAnswer>> GetCandidateAnswersByCandidateIdAsync(Guid candidateId);

        Task DeleteCandidateAnswersByCandidateIdAsync(Guid candidateId);
    }
}
