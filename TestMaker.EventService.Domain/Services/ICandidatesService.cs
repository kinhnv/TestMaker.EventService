using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestMaker.Common.Models;
using TestMaker.EventService.Domain.Models.Candidate;
using TestMaker.EventService.Domain.Models.CandidateAnswer;

namespace TestMaker.EventService.Domain.Services
{
    public interface ICandidatesService
    {
        Task<ServiceResult<GetPaginationResult<CandidateForList>>> GetCandidatesAsync(GetCandidatesParams filter);

        Task<ServiceResult<CandidateForDetails>> GetCandidateAsync(Guid candidateId);

        Task<ServiceResult<CandidateForDetails>> CreateCandidateAsync(CandidateForCreating candidate);

        Task<ServiceResult<CandidateForDetails>> EditCandidateAsync(CandidateForEditing candidate);

        Task<ServiceResult> DeleteCandidateAsync(Guid candidateId);

        Task<ServiceResult<CandidateAnswer>> GetAnswerAsync(Guid candidateId, Guid questionId);

        Task<ServiceResult<List<CandidateAnswer>>> GetAnswersAsync(Guid candidateId);

        Task<ServiceResult> SubmitQuestionAsync(CandidateAnswerForSubmit answer);

        Task<ServiceResult> SubmitCandidateAsync(Guid candidateId);

        Task<ServiceResult> ClearAnswersOfCandidateAsync(Guid candidateId);

        Task<ServiceResult> CreatePreparedTestTempAsync(Guid candidateId, PreparedTest preparedTest);

        Task<ServiceResult<PreparedTest>> GetPreparedTestTempAsync(Guid candidateId);
    }
}
