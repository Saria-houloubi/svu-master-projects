using AMW.Core.IServices;
using AMW.Data.Models.Base;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AMW.Core.Services.Base
{
    public abstract class BaseAmwRepositoryService<T> : BaseAmwLookupRepositoryService<T>, IRepositoryService<T>
        where T : BaseEntity, new()
    {
        #region Poperties
        
        public virtual string InsertOrUpdateProc { get; }
        public virtual string DeleteEntityProc { get; }
        #endregion

        #region Constructer
        /// <summary>
        /// Default constructer
        /// </summary>
        public BaseAmwRepositoryService(IDatabaseExecuterService databaseExecuterService)
            : base(databaseExecuterService)
        {
        }
        #endregion


        public virtual async Task<T> InsertOrUpdateAsync(T entity)
        {
            if (string.IsNullOrEmpty(InsertOrUpdateProc))
            {
                throw new NotImplementedException("Could not add/update record as no function was provided");
            }

            return await databaseExecuterService.RunStoredProcedureAsync(InsertOrUpdateProc, (reader) =>
            {
                var entityObj = new T();

                entityObj.ParseData(reader);

                return entityObj;
            }, GetEntityProperties(entity));
        }

        public virtual async Task<IEnumerable<T>> InsertOrUpdateAsync(IEnumerable<T> entities)
        {
            var entityList = new List<T>();

            foreach (var item in entities)
            {
                entityList.Add(await InsertOrUpdateAsync(item));
            }

            return entityList;
        }

        public virtual async Task<bool> DeleteAsync(T entity)
        {
            if (string.IsNullOrEmpty(DeleteEntityProc))
            {
                throw new NotImplementedException("Could not delete record as no function was provided");
            }

            var recordsAffected = await databaseExecuterService.RunStoredProcedureAsync(DeleteEntityProc, new Dictionary<string, object>()
            {
                {"id" ,entity.Id }
            });

            return recordsAffected > 0;
        }
    }
}
