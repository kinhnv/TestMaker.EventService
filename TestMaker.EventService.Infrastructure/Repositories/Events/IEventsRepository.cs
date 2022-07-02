using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestMaker.Common.Repository;
using TestMaker.EventService.Domain.Models;
using TestMaker.EventService.Infrastructure.Entities;

namespace TestMaker.EventService.Infrastructure.Repositories.Events
{
    public interface IEventsRepository: IRepository<Event>
    {
        Task<EventAndCandidate> GetEventAndCandidateAsync(string eventCode, string candidateCode);

        Task<List<EventAndCandidate>> GetEventsAndCandidatesAsync(EventsAndCandidatesParams p);
    }
}
