﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestMaker.EventService.Domain.Models.Candidate;

namespace TestMaker.EventService.Domain.Models
{
    public class EventsAndCandidatesParams
    {
        public int? Type { get; set; }

        public CandidateStatus? CandidateStatus { get; set; }
    }
}
