using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestMaker.Business.Admin.Domain.Models.Candidate;

namespace TestMaker.Business.Admin.Domain.Services
{
    public interface ICandidatesService
    {
        Task<IEnumerable<CandidateForList>> GetCandidatesAsync(GetCandidateFilter filter);

        Task<CandidateForDetails> GetCandidateAsync(Guid candidateId);

        Task<CandidateForDetails> CreateCandidateAsync(CandidateForCreating candidate);

        Task EditCandidateAsync(CandidateForEditing candidate);

        Task DeleteCandidateAsync(Guid candidateId);

        Task<bool> CandidateExistsAsync(Guid candidateId);
    }
}
