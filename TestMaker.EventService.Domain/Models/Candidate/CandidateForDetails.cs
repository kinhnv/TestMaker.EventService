using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestMaker.EventService.Domain.Models.Candidate
{
    public class CandidateForDetails
    {
        [Required]
        public Guid EventId { get; set; }

        [Required]
        public Guid CandidateId { get; set; }

        [Required]
        [StringLength(8)]
        public string Code { get; set; }
    }
}
