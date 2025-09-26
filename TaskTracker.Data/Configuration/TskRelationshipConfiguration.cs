using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        }
    }
}
