using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestMaker.Common.Models;

namespace TestMaker.EventService.Domain.Models.Candidate
{
    public class GetCandidatesParams : GetPaginationParams
    {
        public GetCandidatesParams()
        {
            EventId = null;
            IsDeleted = false;
        }
        public Guid? EventId { get; set; }

        public bool IsDeleted { get; set; }
    }
}
