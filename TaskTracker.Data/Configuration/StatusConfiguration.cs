using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskTracker.Domain.Entities;

namespace TaskTracker.Data.Configuration
{
    public class StatusConfiguration : IEntityTypeConfiguration<Status>
    {
        public void Configure(EntityTypeBuilder<Status> builder)
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
                    Enum.GetValues(typeof(StatusIdEnum))
                    .Cast<StatusIdEnum>()
                    .Select(f => new Status()
                    { 
                        Id = (int)f,
                        Name = f.ToString()
                    })
                );
        }
    }
}
