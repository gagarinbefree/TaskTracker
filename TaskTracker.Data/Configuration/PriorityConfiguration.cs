using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskTracker.Domain.Entities;

namespace TaskTracker.Data.Configuration
{
    public class PriorityConfiguration : IEntityTypeConfiguration<Priority>
    {
        public void Configure(EntityTypeBuilder<Priority> builder)
        {
            builder
                .HasKey(f => f.Id);

            builder
                .Property(f => f.Id)
                .ValueGeneratedOnAdd()
                .HasColumnType("integer");

            builder
                .Property(f => f.Id)
                .HasConversion<int>();

            builder
                .HasData(
                    Enum.GetValues(typeof(PriorityIdEnum))
                    .Cast<PriorityIdEnum>()
                    .Select(f => new Priority()
                    {
                        Id = (int)f,
                        Name = f.ToString()
                    })
                );
        }
    }
}
