using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestMaker.Business.Admin.Domain.Models.Candidate
{
    public class CandidateForList
    {
        public Guid EventId { get; set; }

        public Guid CandidateId { get; set; }

        [StringLength(8)]
        public string Code { get; set; }
    }
}
