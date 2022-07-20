using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestMaker.Common.Repository;
using TestMaker.EventService.Infrastructure.Entities;

namespace TestMaker.EventService.Infrastructure.Repositories.Candidates
{
    public class CandidatesRepository : Repository<Candidate>, ICandidatesRepository
    {
        public CandidatesRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<Event> GetEventByCandidateId(Guid candidateId)
        {
            var eventAsQueryable = _dbContext.Set<Event>().Where(x => !x.IsDeleted);
            var candidateAsQueryable = _dbContext.Set<Candidate>().Where(x => !x.IsDeleted && x.CandidateId == candidateId);

            var @event = (from e in eventAsQueryable
                          join c in candidateAsQueryable on e.EventId equals c.EventId
                          select e).Single();

            return await Task.FromResult(@event);
        }
    }
}
