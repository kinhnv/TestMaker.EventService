using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestMaker.Business.Admin.Domain.Models.Event
{
    public class EventForCreating
    {
        [Required]
        [StringLength(64)]
        public string Name { get; set; }

        [Required]
        public int Type { get; set; }

        [Required]
        public Guid TestId { get; set; }

    }
}
