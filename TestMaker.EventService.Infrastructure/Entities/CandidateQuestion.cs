using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestMaker.EventService.Infrastructure.Entities
{
    public class CandidateQuestion
    {
        public Guid CandidateId { get; set; }

        public Guid QuestionId { get; set; }

        public int Type { get; set; }

        public string QuestionAsJson { get; set; }

        public int NumericalOrder { get; set; }
    }
}
