using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestMaker.Business.Admin.Domain.Models.Candidate
{
    public class CandidateForCreating
    {
        [Required]
        public Guid EventId { get; set; }
    }
}
