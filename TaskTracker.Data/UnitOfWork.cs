using TaskTracker.Data.Repositories;
using TaskTracker.Domain;
using TaskTracker.Domain.Repositories;

namespace TaskTracker.Data
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly ServiceDbContext _context;
        private ITskRepository _tskRepository;
        private ITskRelationshipRepository _tskRelationshipRepository;

        public UnitOfWork(ServiceDbContext context)
        {
            _context = context;
            _tskRepository = new TskRepository(_context);
            _tskRelationshipRepository = new TskRelationshipRepository(_context);

        }

        public ITskRepository Tsk => new TskRepository(_context);

        public ITskRelationshipRepository TskRelationship => new TskRelationshipRepository(_context);

        public async Task<int> CommitAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
