using Microsoft.EntityFrameworkCore;
using SmartEvent.Application.Interfaces;
using SmartEvent.Infrastructure.Persistence;

namespace SmartEvent.Infrastructure.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly SmartEventDbContext _context;
        protected readonly DbSet<T> _dbSet;
        public Repository(SmartEventDbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }
        public async Task<T?> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public void Update(T entity)
        {
            _dbSet.Update(entity);
        }

        public void Remove(T entity)
        {
            _dbSet.Remove(entity);
        }
    }
}
