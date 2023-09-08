using System.Threading.Tasks;
using System.Collections.Generic;

namespace Template.DataAccess.Repositories.Interfaces
{
    public interface IRepositoryBase<TEntity> where TEntity : class, new()
    {
        Task<IEnumerable<TEntity>> GetAllAsync(int skip, int take);
        Task<TEntity> GetByIdAsync(int id);
        Task<TEntity> CreateAsync(TEntity entity);
        Task<TEntity> UpdateAsync(TEntity entity, int id);
        Task<bool> DeleteAsync(int id);
    }
}
