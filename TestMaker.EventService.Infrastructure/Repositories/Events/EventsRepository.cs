using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestMaker.EventService.Domain.Models;
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
            var data = from e in _dbContext.Events.Where(x => x.Code == eventCode)
                       join c in _dbContext.Candidates.Where(x => x.Code == candidateCode) on e.EventId equals c.EventId
                       select new EventAndCandidate
                       {
                           Event = e,
                           Candidate = c
                       };

            return await Task.FromResult(data.FirstOrDefault());
        }

        public async Task<List<EventAndCandidate>> GetEventsAndCandidatesAsync(EventsAndCandidatesParams p)
        {
            var events = _dbContext.Events.AsQueryable();
            if (p.Type != null)
            {
                events = events.Where(e => e.Type == p.Type);
            }
            var candidates = _dbContext.Candidates.AsQueryable();
            if (p.CandidateStatus != null)
            {
                candidates = candidates.Where(e => e.Status == (int)p.CandidateStatus);
            }
            var data = from e in events
                       join c in candidates on e.EventId equals c.EventId into c2
                       from c in c2.DefaultIfEmpty()
                       select new EventAndCandidate
                       {
                           Event = e,
                           Candidate = c
                       };

            return await Task.FromResult(data.ToList());
        }
    }
}
