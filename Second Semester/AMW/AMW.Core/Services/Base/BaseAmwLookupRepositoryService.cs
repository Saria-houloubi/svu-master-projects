using AMW.Core.IServices;
using AMW.Data.Abstraction.Sorting;
using AMW.Data.Models.Base;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AMW.Core.Services.Base
{
    public class BaseAmwLookupRepositoryService<T> : BaseAmwRepositoryService, ILookUpRepository<T>
        where T : BaseEntity, new()
    {
        #region Properties
        public virtual string GetByIdProc { get; }
        public virtual string GetByFilterProc { get; }

        #endregion

        #region Constructer
        /// <summary>
        /// Default consstructer
        /// </summary>
        /// <param name="databaseExecuterService"></param>
        public BaseAmwLookupRepositoryService(IDatabaseExecuterService databaseExecuterService)
            : base(databaseExecuterService)
        {
        }
        #endregion

        public virtual async Task<IEnumerable<T>> GetByFilterAsync(BaseEntityFilter filter, ISorter<T> sorter = null)
        {
            if (string.IsNullOrEmpty(GetByFilterProc))
            {
                throw new NotImplementedException("Could not get record by filter as no function was provided");
            }

            return await databaseExecuterService.RunStoredProcedureAsync(GetByFilterProc, (reader) =>
            {
                var filteredList = new List<T>();

                do
                {
                    if (reader.HasRows)
                    {
                        var entity = new T();

                        entity.ParseData(reader);

                        filteredList.Add(entity);
                    }
                } while (reader.Reader.Read()); //we activate the read after as the first one is done in the base DB class

                if (sorter != null)
                {
                    filteredList = new List<T>(sorter.Sort(filteredList));
                }
                return filteredList;

            }, GetEntityProperties(filter));
        }
        public virtual async Task<T> GetByIdAsync(int id)
        {

            if (string.IsNullOrEmpty(GetByIdProc))
            {
                throw new NotImplementedException("Could not get record by Id as no function was provided");
            }

            return await databaseExecuterService.RunStoredProcedureAsync(GetByIdProc, (reader) =>
            {
                var entity = new T();

                entity.ParseData(reader);

                return entity;
            }, new Dictionary<string, object>()
            {
                {nameof(id),id }
            });
        }
    }
}
