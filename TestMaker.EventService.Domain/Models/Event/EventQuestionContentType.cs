using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestMaker.Common.Attributes;

namespace TestMaker.EventService.Domain.Models.Event
{
    public enum EventQuestionContentType
    {
        [EnumName("Nguyên bản")]
        Origin = 0,
        [EnumName("Đổi vị trí câu hỏi")]
        RandomAll = 1,
        [EnumName("Chọn x phần trăm câu hỏi")]
        RandomWithPercent = 2,
        [EnumName("Chọn x câu hỏi")]
        RandomWithConstant = 3,
        [EnumName("Chọn câu hỏi thông minh")]
        SmartRandom = 4
    }
}
