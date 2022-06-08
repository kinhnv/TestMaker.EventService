﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestMaker.EventService.Domain.Models;
using TestMaker.EventService.Domain.Models.Candidate;

namespace TestMaker.EventService.Domain.Services
{
    public interface ICandidatesService
    {
        Task<IEnumerable<CandidateForList>> GetCandidatesAsync(GetCandidateFilter filter);

        Task<CandidateForDetails> GetCandidateAsync(Guid candidateId);

        Task<CandidateForDetails> CreateCandidateAsync(CandidateForCreating candidate);

        Task EditCandidateAsync(CandidateForEditing candidate);

        Task DeleteCandidateAsync(Guid candidateId);

        Task<bool> CandidateExistsAsync(Guid candidateId);

        Task<List<CandidateAnswer>> GetAnswerAsync(Guid candidateId, Guid questionId);

        Task<List<TestMaker.EventService.Domain.Models.CandidateAnswer>> GetAnswersAsync(Guid candidateId);

        Task SubmitAnswerAsync(CandidateAnswerForSubmit answer);
    }
}
