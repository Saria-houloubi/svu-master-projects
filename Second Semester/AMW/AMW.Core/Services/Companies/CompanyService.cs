using AMW.Core.IServices;
using AMW.Core.Services.Base;
using AMW.Data.Abstraction.Sorting;
using AMW.Data.Models.Base;
using AMW.Data.Models.Companies;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AMW.Core.Services.Companies
{
    public partial class CompanyService : BaseAmwRepositoryService<Company>
    {
        #region Constructer
        /// <summary>
        /// Default constructer
        /// </summary>
        public CompanyService(IDatabaseExecuterService databaseExecuterService)
            : base(databaseExecuterService)
        {

        }
        #endregion

        public override async Task<IEnumerable<Company>> GetByFilterAsync(BaseEntityFilter filter, ISorter<Company> sorter = null)
        {
            return await databaseExecuterService.RunStoredProcedureAsync(GetByFilterProc, (reader) =>
            {
                var filteredList = new List<CompanyRegister>();

                do
                {
                    if (reader.HasRows)
                    {
                        var entity = new CompanyRegister();

                        entity.ParseData(reader);

                        filteredList.Add(entity);
                    }
                } while (reader.Reader.Read()); //we activate the read after as the first one is done in the base DB class
                return filteredList;

            }, GetEntityProperties(filter));
        }
    }
}
