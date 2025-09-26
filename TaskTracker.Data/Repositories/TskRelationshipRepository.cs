using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskTracker.Domain.Entities;
using TaskTracker.Domain.Repositories;

namespace TaskTracker.Data.Repositories
{
    public class TskRelationshipRepository : Repository<TskRelationship>, ITskRelationshipRepository
    {
        public TskRelationshipRepository(ServiceDbContext context)
           : base(context)
        {
        }
    }
}
