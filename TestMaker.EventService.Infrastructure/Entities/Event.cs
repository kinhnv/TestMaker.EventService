using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestMaker.EventService.Infrastructure.Attributes;

namespace TestMaker.EventService.Infrastructure.Entities
{
    public enum EventType
    {
        [Name("Bảo mật")]
        Private = 0,
        [Name("Công khai")]
        Public = 1,
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
        public EventType TypeAsEnum
        {
            get { return (EventType)Type; }
            set { Type = (int)value; }
        }

        [Required]
        public int Type { get; set; }

        [Required]
        public Guid TestId { get; set; }

        [Required]
        public bool IsDeleted { get; set; }
    }
}
