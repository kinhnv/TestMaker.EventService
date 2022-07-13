using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestMaker.EventService.Domain.Models.Candidate
{
    public class PreparedTest
    {
        public class PreparedSection
        {
            public class PreparedQuestion
            {
                [JsonProperty("questionId")]
                public Guid QuestionId { get; set; }

                [JsonProperty("type")]
                public int Type { get; set; }

                [JsonProperty("media")]
                public string Media { get; set; }

                [JsonProperty("questionAsJson")]
                public string QuestionAsJson { get; set; }
            }

            [JsonProperty("sectionId")]
            public Guid SectionId { get; set; }

            [JsonProperty("name")]
            public string Name { get; set; }

            [JsonProperty("questions")]
            public IEnumerable<PreparedQuestion> Questions { get; set; }
        }

        [JsonProperty("testId")]
        public Guid TestId { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("sections")]
        public IEnumerable<PreparedSection> Sections { get; set; }
    }
}
