using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestMaker.EventService.Domain.Models.Event
{
    public class GetEventsFilter
    {
        public bool IsDeleted { get; set; } = false;
    }
}
