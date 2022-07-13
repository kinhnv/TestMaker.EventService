using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestMaker.Common.Attributes;
using TestMaker.Common.Repository;

namespace TestMaker.EventService.Infrastructure.Entities
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

    public class Event : Entity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid EventId { get; set; }

        [Required]
        [StringLength(64)]
        public string Name { get; set; }

        [Required]
        [StringLength(8)]
        public string Code { get; set; }

        [NotMapped]
        public EventScopeType ScopeTypeAsEnum
        {
            get { return (EventScopeType)ScopeType; }
            set { ScopeType = (int)value; }
        }

        [Required]
        public int ScopeType { get; set; }

        [NotMapped]
        public EventQuestionContentType QuestionContentTypeAsEnum
        {
            get { return (EventQuestionContentType)QuestionContentType; }
            set { QuestionContentType = (int)value; }
        }

        [Required]
        public int QuestionContentType { get; set; }

        [Required]
        public Guid TestId { get; set; }
    }
}
