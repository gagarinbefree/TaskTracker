using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskTracker.Domain.Entities;

namespace TaskTracker.Domain.Repositories
{
    public interface ITskRepository : IRepository<Tsk>
    {
    }
}
