﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestMaker.EventService.Infrastructure.Entities;
using TestMaker.EventService.Infrastructure.Repository;

namespace TestMaker.EventService.Infrastructure.Repositories.Candidates
{
    public class CandidatesRepository : Repository<Candidate>, ICandidatesRepository
    {
        public CandidatesRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
