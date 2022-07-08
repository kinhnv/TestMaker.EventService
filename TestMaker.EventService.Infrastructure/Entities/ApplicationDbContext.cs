using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestMaker.EventService.Infrastructure.Entities
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
            modelBuilder.Entity<Event>().Property(e => e.ScopeType).HasDefaultValue((int)EventScopeType.Private);
            modelBuilder.Entity<Event>().Property(e => e.IsDeleted).HasDefaultValue(false);

            modelBuilder.Entity<Candidate>().HasKey(c => c.CandidateId);
            modelBuilder.Entity<Candidate>().Property(e => e.IsDeleted).HasDefaultValue(false);
            modelBuilder.Entity<Candidate>().Property(e => e.CreatedAt).HasDefaultValue(new DateTime(2022, 1, 1));

            modelBuilder.Entity<CandidateAnswer>().HasKey(ca => new
            {
                ca.CandidateId,
                ca.QuestionId
            });
        }
    }
}
