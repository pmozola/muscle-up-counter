using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MUCounter.Application.Domain;

namespace MUCounter.Database.Congiguration
{
    internal class RepetitionEntityConfiguration : IEntityTypeConfiguration<MuscleUpRepetition>
    {
        public void Configure(EntityTypeBuilder<MuscleUpRepetition> builder)
        {
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.Date).IsRequired();
        }
    }
}
