using AMW.Data.Models.Base;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AMW.Core.IServices
{
    public interface IRepositoryService<TEntity>
        where TEntity : BaseEntity
    {
        Task<TEntity> GetByIdAsync(int id);

        Task<IEnumerable<TEntity>> GetByFilterAsync(BaseEntityFilter filter);

        Task<TEntity> InsertOrUpdateAsync(TEntity entity);

        Task<IEnumerable<TEntity>> InsertOrUpdateAsync(IEnumerable<TEntity> entities);

    }
}
