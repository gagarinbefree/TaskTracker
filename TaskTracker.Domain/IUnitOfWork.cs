using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskTracker.Domain.Repositories;

namespace TaskTracker.Domain
{
    public interface IUnitOfWork
    {
        ITskRepository Tsk { get; }
        ITskRelationshipRepository TskRelationship { get; }
        Task<int> CommitAsync();
    }
}
