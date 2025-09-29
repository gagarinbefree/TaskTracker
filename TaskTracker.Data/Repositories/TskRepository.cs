using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskTracker.Domain.Entities;
using TaskTracker.Domain.Repositories;

namespace TaskTracker.Data.Repositories
{
    public class TskRepository : Repository<Tsk> , ITskRepository
    {
        public TskRepository(ServiceDbContext context)
           : base(context)
        { 

        }        
    }
}