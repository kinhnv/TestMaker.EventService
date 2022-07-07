using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestMaker.Common.Repository;
using TestMaker.EventService.Infrastructure.Attributes;

namespace TestMaker.EventService.Infrastructure.Entities
{
    public enum EventScopeType
    {
        [Name("Bảo mật")]
        Private = 0,
        [Name("Công khai")]
        Public = 1,
        [Name("Công khai với một vài người dùng")]
        Protected = 3
    }

    public enum EventContentType
    {
        [Name("Nguyên bản")]
        Origin = 0,
        [Name("Đổi vị trí câu hỏi")]
        RandomAll = 1,
        [Name("Chọn x phần trăm câu hỏi")]
        RandomWithPercent = 2,
        [Name("Chọn x câu hỏi")]
        RandomWithConstant = 3,
        [Name("Chọn câu hỏi thông minh")]
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
        public EventContentType QuestionContentTypeAsEnum
        {
            get { return (EventContentType)QuestionContentType; }
            set { QuestionContentType = (int)value; }
        }

        [Required]
        public int QuestionContentType { get; set; }

        [Required]
        public Guid TestId { get; set; }
    }
}
