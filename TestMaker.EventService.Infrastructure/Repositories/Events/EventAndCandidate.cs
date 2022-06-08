using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestMaker.EventService.Infrastructure.Entities;

namespace TestMaker.EventService.Infrastructure.Repositories.Events
{
    public class EventAndCandidate
    {
        public Event Event { get; set; }

        public Candidate Candidate { get; set; }
    }
}
