using AMW.Core.IServices;
using AMW.Core.Services.Base;
using AMW.Data.Models.Base;
using AMW.Data.Models.Companies;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AMW.Core.Services.Companies
{
    public partial class CompanyService : BaseAmwRepositoryService, IRepositoryService<Company>
    {
        #region Poperties
        private readonly IDatabaseExecuterService databaseExecuterService;
        #endregion

        #region Constructer
        /// <summary>
        /// Default constructer
        /// </summary>
        public CompanyService(IDatabaseExecuterService databaseExecuterService)
        {
            this.databaseExecuterService = databaseExecuterService;
        }
        #endregion
        public async Task<Company> GetByIdAsync(int id)
        {
            return await databaseExecuterService.RunStoredProcedureAsync(GetByIdProc, (reader) =>
            {
                var candidate = new Company();

                candidate.ParseData(reader);

                return candidate;
            }, new Dictionary<string, object>()
            {
                {nameof(id),id }
            });
        }

        public async Task<IEnumerable<Company>> GetByFilterAsync(BaseEntityFilter filter)
        {
            return await databaseExecuterService.RunStoredProcedureAsync(GetByFilterProc, (reader) =>
            {
                var filteredList = new List<CompanyRegister>();

                do
                {
                    if (reader.HasRows)
                    {
                        var candidate = new CompanyRegister();

                        candidate.ParseData(reader);

                        filteredList.Add(candidate);
                    }
                } while (reader.Reader.Read()); //we activate the read after as the first one is done in the base DB class
                return filteredList;

            }, GetEntityProperties(filter));
        }

        public async Task<Company> InsertOrUpdateAsync(Company entity)
        {
            return await databaseExecuterService.RunStoredProcedureAsync(InsertOrUpdateCandiateProc, (reader) =>
            {
                var candidate = new Company();

                candidate.ParseData(reader);

                return candidate;
            }, GetEntityProperties(entity));
        }

        public async Task<IEnumerable<Company>> InsertOrUpdateAsync(IEnumerable<Company> entities)
        {
            var candidates = new List<Company>();

            foreach (var item in entities)
            {
                candidates.Add(await InsertOrUpdateAsync(item));
            }

            return candidates;
        }
    }
}
