using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestMaker.EventService.Domain.Models.CandidateAnswer
{
    public class CandidateAnswerForSubmit
    {
        public Guid CandidateId { get; set; }

        public Guid QuestionId { get; set; }

        public string AnswerAsJson { get; set; }

        public int? CandidateAnswerStatus { get; set; }

        public bool Marking { get; set; } = false;
    }
}
