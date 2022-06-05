using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestMaker.EventService.Infrastructure.Entities
{
    public class CandidateAnswer : Entity
    {
        [Required]
        public Guid CandidateId { get; set; }

        [Required]
        public Guid QuestionId { get; set; }

        [Required]
        public string AnswerAsJson { get; set; }
    }
}
