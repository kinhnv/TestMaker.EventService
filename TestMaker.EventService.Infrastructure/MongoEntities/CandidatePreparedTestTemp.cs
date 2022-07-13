using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestMaker.Common.Mongodb;
using TestMaker.EventService.Domain.Models.Candidate;

namespace TestMaker.EventService.Infrastructure.MongoEntities
{
    public class CandidatePreparedTestTemp : MongoEntity
    {
        public Guid CandidateId { get; set; }

        public PreparedTest PreparedTest { get; set; }
    }
}
