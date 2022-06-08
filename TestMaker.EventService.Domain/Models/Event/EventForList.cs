using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestMaker.EventService.Domain.Models.Event
{
    public class EventForList
    {
        public Guid EventId { get; set; }

        public string Code { get; set; }

        public string Name { get; set; }

        public int Type { get; set; }

        public Guid TestId { get; set; }
    }
}
