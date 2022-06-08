using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestMaker.EventService.Domain.Models
{
    public class CandidateAnswer
    {
        public Guid QuestionId { get; set; }

        public string AnswerAsJson { get; set; }
    }
}
