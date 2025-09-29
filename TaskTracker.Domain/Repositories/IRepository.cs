using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace TaskTracker.Domain.Repositories
{
    public interface IRepository<TEntity> where TEntity : class
    {
        Task<TEntity?> GetByIdAsync(int id);
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task AddAsync(TEntity entity);
        void Remove(TEntity entity);
        Task<bool> RemoveByIdAsync(int id);
        Task<IEnumerable<TEntity>> GetAllIncludeAsync(Expression<Func<TEntity, object>> include);
        Task<TEntity?> GetByExpressionIncludeAsync(Expression<Func<TEntity, bool>> expression, Expression<Func<TEntity, object>> include);
    }
}
