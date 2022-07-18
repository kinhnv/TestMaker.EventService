using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestMaker.Common.Attributes;
using TestMaker.Common.Repository;
using TestMaker.EventService.Domain.Models.Event;

namespace TestMaker.EventService.Infrastructure.Entities
{
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

        [NotMapped]
        public EventMarkingType MarkingTypeAsEnum
        {
            get { return (EventMarkingType)MarkingType; }
            set { MarkingType = (int)value; }
        }

        [Required]
        public int MarkingType { get; set; }

        [Required]
        public Guid TestId { get; set; }
    }
}
