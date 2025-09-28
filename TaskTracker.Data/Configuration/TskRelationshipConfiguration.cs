using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskTracker.Domain.Entities;

namespace TaskTracker.Data.Configuration
{
    class TskRelationshipConfiguration : IEntityTypeConfiguration<TskRelationship>
    {
        public void Configure(EntityTypeBuilder<TskRelationship> builder)
        {
            builder
                .HasKey(f => f.Id);

            builder
              .Property(f => f.Id)
              .ValueGeneratedOnAdd()
              .HasColumnType("integer");

            builder.Property(f => f.TypeId)
                .HasConversion<int>();
        }
    }
}