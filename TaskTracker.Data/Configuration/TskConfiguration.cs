using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskTracker.Domain.Entities;

namespace TaskTracker.Data.Configuration
{
    class TskConfiguration : IEntityTypeConfiguration<Tsk>
    {
        public void Configure(EntityTypeBuilder<Tsk> builder)
        {
            builder
                .HasKey(f => f.Id);

            builder
              .Property(f => f.Id)
              .ValueGeneratedOnAdd()
              .HasColumnType("integer");

            builder
                .Property(f => f.Title)
                .IsRequired()
                .HasMaxLength(250);

            builder.HasMany(f => f.SubTasks)
                .WithOne()
                .HasForeignKey(f => f.ParentTaskId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(f => f.RelatedTasks)
                .WithOne(f => f.SourceTask)
                .HasForeignKey(f => f.SourceTaskId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Property(f => f.StatusId)
                .HasConversion<int>();

            builder.Property(f => f.PriorityId)
                .HasConversion<int>();
        }
    }
}
