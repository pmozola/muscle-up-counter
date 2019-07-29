using Microsoft.EntityFrameworkCore;
using MUCounter.Application.Domain;
using MUCounter.Database.Congiguration;

namespace MUCounter.Database
{
    public class MUCDatabaseContext : DbContext
    {
        public MUCDatabaseContext(DbContextOptions<MUCDatabaseContext> options)
          : base(options)
        {
        }

        public DbSet<MuscleUpRepetition> Repetitions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new RepetitionEntityConfiguration());
        }
    }
}
