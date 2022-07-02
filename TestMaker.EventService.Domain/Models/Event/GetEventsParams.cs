using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestMaker.Common.Models;

namespace TestMaker.EventService.Domain.Models.Event
{
    public class GetEventsParams : GetPaginationParams
    {
        public bool IsDeleted { get; set; } = false;
    }
}
