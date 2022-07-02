using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestMaker.Common.Models;
using TestMaker.EventService.Domain.Models;
using TestMaker.EventService.Domain.Models.Candidate;

namespace TestMaker.EventService.Domain.Services
{
    public interface ICandidatesService
    {
        Task<ServiceResult<GetPaginationResult<CandidateForList>>> GetCandidatesAsync(GetCandidatesParams filter);

        Task<ServiceResult<CandidateForDetails>> GetCandidateAsync(Guid candidateId);

        Task<ServiceResult<CandidateForDetails>> CreateCandidateAsync(CandidateForCreating candidate);

        Task<ServiceResult<CandidateForDetails>> EditCandidateAsync(CandidateForEditing candidate);

        Task<ServiceResult> DeleteCandidateAsync(Guid candidateId);

        Task<ServiceResult<string>> GetAnswerAsync(Guid candidateId, Guid questionId);

        Task<ServiceResult<List<TestMaker.EventService.Domain.Models.CandidateAnswer>>> GetAnswersAsync(Guid candidateId);

        Task<ServiceResult> SubmitQuestionAsync(CandidateAnswerForSubmit answer);

        Task<ServiceResult> SubmitCandidateAsync(Guid candidateId);

        Task<ServiceResult> ClearAnswersOfCandidateAsync(Guid candidateId);
    }
}
