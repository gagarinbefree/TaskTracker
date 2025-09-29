using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TaskTracker.Domain.Repositories;

namespace TaskTracker.Data.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly DbContext _context;

        public Repository(DbContext context)
        {
            _context = context;
        }
        
        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await _context.Set<TEntity>().ToListAsync();
        }

        public async Task<IEnumerable<TEntity>> GetAllIncludeAsync(Expression<Func<TEntity, object>> include)
        {
            return await _context.Set<TEntity>().Include(include).ToListAsync();
        }

        public async Task<TEntity?> GetByIdAsync(int id)
        {
            return await _context.Set<TEntity>().FindAsync(id);
        }

        public async Task<TEntity?> GetByExpressionIncludeAsync(Expression<Func<TEntity, bool>> expression, Expression<Func<TEntity, object>> include)
        {
            return await _context.Set<TEntity>().Include(include).FirstOrDefaultAsync(expression);
        }

        public async Task AddAsync(TEntity entity)
        {
            await _context.Set<TEntity>().AddAsync(entity);
        }

        public void Remove(TEntity entity)
        {
            _context.Set<TEntity>().Remove(entity);
        }

        public async Task<bool> RemoveByIdAsync(int id)
        {
            var entity = await GetByIdAsync(id);
            if (entity == null)
                return false;

            Remove(entity);

            return true;
        }
    }
}
