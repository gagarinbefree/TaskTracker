using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using TaskTracker.Data.Configuration;

namespace TaskTracker.Data
{
    public class ServiceDbContext : DbContext
    {
        public ServiceDbContext(DbContextOptions<ServiceDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new StatusConfiguration());
            builder.ApplyConfiguration(new PriorityConfiguration());
            builder.ApplyConfiguration(new RelationshipTypeConfiguration());
            builder.ApplyConfiguration(new TskConfiguration());
            builder.ApplyConfiguration(new TskRelationshipConfiguration());            
        }
    }
}

