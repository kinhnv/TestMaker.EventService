using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TM.EventService.Infrastructure.Entities
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options)
        {
        }

        public DbSet<Event> Events { get; set; }

        public DbSet<Candidate> Candidates { get; set; }

        public DbSet<CandidateAnswer> CandidateAnswers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Event>().HasKey(e => e.EventId);
            modelBuilder.Entity<Event>().Property(e => e.Type).HasDefaultValue((int)EventType.Private);

            modelBuilder.Entity<Candidate>().HasKey(c => c.CandidateId);

            modelBuilder.Entity<CandidateAnswer>().HasKey(ca => new
            {
                ca.CandidateId,
                ca.QuestionId
            });
        }
    }
}
