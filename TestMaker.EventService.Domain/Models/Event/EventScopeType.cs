using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestMaker.Common.Attributes;

namespace TestMaker.EventService.Domain.Models.Event
{
    public enum EventScopeType
    {
        [EnumName("Bảo mật")]
        Private = 0,
        [EnumName("Công khai")]
        Public = 1,
        [EnumName("Công khai với một vài người dùng")]
        Protected = 3
    }
}
