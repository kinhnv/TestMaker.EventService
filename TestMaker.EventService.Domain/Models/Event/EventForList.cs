﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestMaker.EventService.Domain.Models.Event
{
    public class EventForList
    {
        public Guid EventId { get; set; }

        public string Code { get; set; }

        public string Name { get; set; }

        public string ScopeType { get; set; }

        public string QuestionContentType { get; set; }

        public string MarkingType { get; set; }
    }
}
