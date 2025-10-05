
using System.Linq.Expressions;
using MemosWebApplicationCore.Entites;

namespace MemosWebApplicationCore.Interfaces
{
    public interface IAsyncRepository<TBaseEntity> where TBaseEntity : BaseEntity
    {
        Task<TBaseEntity?> GetByIdAsync(int id);
        Task<IEnumerable<TBaseEntity>> ListAsync();
        Task<IEnumerable<TBaseEntity>> ListAsync(Expression<Func<TBaseEntity, bool>> predicate);
        Task<TBaseEntity> AddAsync(TBaseEntity entity);
        Task DeleteAsync(TBaseEntity entity);
        Task<TBaseEntity> EditAsync(TBaseEntity entity);
    }
}
