using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestMaker.Common.Mongodb;
using TestMaker.EventService.Infrastructure.MongoEntities;

namespace TestMaker.EventService.Infrastructure.Repositories.Candidates
{
    public interface ICandidatePreparedTestTempsRepository : IMongoRepository<CandidatePreparedTestTemp>
    {
    }
}
