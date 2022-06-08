using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestMaker.EventService.Infrastructure.Entities;
using TestMaker.EventService.Infrastructure.Repository;

namespace TestMaker.EventService.Infrastructure.Repositories.Events
{
    public interface IEventsRepository: IRepository<Event>
    {
        Task<EventAndCandidate> GetEventAndCandidateAsync(string eventCode, string candidateCode);
    }
}
