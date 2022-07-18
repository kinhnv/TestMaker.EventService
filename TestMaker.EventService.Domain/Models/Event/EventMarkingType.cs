using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestMaker.Common.Attributes;

namespace TestMaker.EventService.Domain.Models.Event
{
    public enum EventMarkingType
    {
        [EnumName("Toàn bộ bài kiểm tra")]
        MarkingAllTest = 0,
        [EnumName("Chấm từng phần")]
        MarkingPerSection = 1,
        [EnumName("Chấm từng câu hỏi")]
        MarkingPerQuestion = 2
    }
}
