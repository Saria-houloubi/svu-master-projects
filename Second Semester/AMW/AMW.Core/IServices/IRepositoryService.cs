using AMW.Data.Models.Base;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AMW.Core.IServices
{
    public interface IRepositoryService<TEntity> : ILookUpRepository<TEntity>
        where TEntity : BaseEntity
    {
        Task<TEntity> InsertOrUpdateAsync(TEntity entity);

        Task<IEnumerable<TEntity>> InsertOrUpdateAsync(IEnumerable<TEntity> entities);

        Task<bool> DeleteAsync(TEntity entity);

    }
}
