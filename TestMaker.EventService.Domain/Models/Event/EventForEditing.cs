using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestMaker.EventService.Domain.Models.Event
{
    public class EventForEditing
    {
        [Required]
        public Guid EventId { get; set; }

        [Required]
        [StringLength(64)]
        public string Name { get; set; }

        [Required]

        public int ScopeType { get; set; }

        [Required]
        public int QuestionContentType { get; set; }

        [Required]
        public Guid TestId { get; set; }
    }
}
