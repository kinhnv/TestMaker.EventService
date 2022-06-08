using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestMaker.EventService.Infrastructure.Entities;
using TestMaker.EventService.Infrastructure.Repository;

namespace TestMaker.EventService.Infrastructure.Repositories.Events
{
    public class EventsRepository : Repository<Event>, IEventsRepository
    {
        public EventsRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<EventAndCandidate> GetEventAndCandidateAsync(string eventCode, string candidateCode)
        {
            var data = from e in _dbContext.Events
                       join c in _dbContext.Candidates on e.EventId equals c.EventId
                       select new EventAndCandidate
                       {
                           Event = e,
                           Candidate = c
                       };

            return await Task.FromResult( data.FirstOrDefault());
        }
    }
}
