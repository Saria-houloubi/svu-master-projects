using AMW.Data.Models.Base;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AMW.Core.IServices
{
    public interface IRepositoryService<TEntity>
        where TEntity : BaseEntity
    {

        TEntity GetById(int id);

        Task<TEntity> GetByIdAsync(int id);

        Task<TEntity> InsertOrUpdateAsync(TEntity entity);

        Task<IEnumerable<TEntity>> InsertOrUpdateAsync(IEnumerable<TEntity> entities);

    }
}
