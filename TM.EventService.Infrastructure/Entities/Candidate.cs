using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TM.EventService.Infrastructure.Entities
{
    public class Candidate : Entity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid CandidateId { get; set; }

        [Required]
        [StringLength(8)]
        public string Code { get; set; }

        public int Status { get; set; }

        [Required]
        public Guid EventId { get; set; }
    }
}
