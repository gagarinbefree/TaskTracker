using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskTracker.Domain.Entities;

namespace TaskTracker.Data.Configuration
{
    public class RelationshipTypeConfiguration : IEntityTypeConfiguration<RelationshipType>
    {
        public void Configure(EntityTypeBuilder<RelationshipType> builder)
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
                    Enum.GetValues(typeof(RelationshipTypeIdEnum))
                    .Cast<RelationshipTypeIdEnum>()
                    .Select(f => new RelationshipType()
                    {
                        Id = (int)f,
                        Name = f.ToString()
                    })
                );
        }
    }
}
