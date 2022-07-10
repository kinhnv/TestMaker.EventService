using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestMaker.EventService.Infrastructure.Entities
{
    public class CandidateSection
    {
        public Guid CandidateId { get; set; }

        public Guid SectionId { get; set; }

        public int NumericalOrder { get; set; }
    }
}
