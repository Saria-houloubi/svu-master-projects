using AMW.Data.Models.Base;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AMW.Core.IServices
{
    public interface ILookUpRepository<T>
        where T : BaseEntity
    {
        Task<T> GetByIdAsync(int id);

        Task<IEnumerable<T>> GetByFilterAsync(BaseEntityFilter filter);
    }
}
