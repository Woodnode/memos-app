
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using MemosWebApplicationCore.Entites;
using MemosWebApplicationCore.Interfaces;

namespace MemosWebInfrastructure.Data
{
    public class AsyncRepository<TBaseEntity> : IAsyncRepository<TBaseEntity> where TBaseEntity : BaseEntity
    {
        protected readonly MemosBdContext _context;
        protected readonly DbSet<TBaseEntity> _dbSet;

        public AsyncRepository(MemosBdContext context)
        {
            _context = context;
            _dbSet = context.Set<TBaseEntity>();
        }

        public virtual async Task<TBaseEntity?> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public virtual async Task<IEnumerable<TBaseEntity>> ListAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public virtual async Task<IEnumerable<TBaseEntity>> ListAsync(Expression<Func<TBaseEntity, bool>> predicate)
        {
            return await _dbSet.Where(predicate).ToListAsync();
        }

        public virtual async Task<TBaseEntity> AddAsync(TBaseEntity entity)
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public virtual async Task DeleteAsync(TBaseEntity entity)
        {
            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public virtual async Task<TBaseEntity> EditAsync(TBaseEntity entity)
        {
            _dbSet.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
    }
}

