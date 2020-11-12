using AMW.Core.IServices;
using AMW.Data.Models.Base;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AMW.Core.Services.Base
{
    public abstract class BaseAmwRepositoryService<T> : BaseAmwRepositoryService, IRepositoryService<T>
        where T : BaseEntity, new()
    {
        #region Poperties
        protected readonly IDatabaseExecuterService databaseExecuterService;

        public virtual string GetByIdProc { get; }
        public virtual string GetByFilterProc { get; }
        public virtual string InsertOrUpdateProc { get; }
        public virtual string DeleteEntityProc { get; }
        #endregion

        #region Constructer
        /// <summary>
        /// Default constructer
        /// </summary>
        public BaseAmwRepositoryService(IDatabaseExecuterService databaseExecuterService)
        {
            this.databaseExecuterService = databaseExecuterService;
        }
        #endregion
        public virtual async Task<T> GetByIdAsync(int id)
        {
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

        public virtual async Task<IEnumerable<T>> GetByFilterAsync(BaseEntityFilter filter)
        {
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
                return filteredList;

            }, GetEntityProperties(filter));
        }

        public virtual async Task<T> InsertOrUpdateAsync(T entity)
        {
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
