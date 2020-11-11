using System.Collections.Generic;
using System.Threading.Tasks;
using AMW.Core.IServices;
using AMW.Core.Services.Base;
using AMW.Data.Models.Base;
using AMW.Data.Models.Candidates;

namespace AMW.Core.Services.Candidates
{
    public partial class CandidateService : BaseAmwRepositoryService<Candidate>
    {
        #region Constructer
        public CandidateService(IDatabaseExecuterService databaseExecuterService)
            : base(databaseExecuterService)
        {
        }
        #endregion

        public override async Task<IEnumerable<Candidate>> GetByFilterAsync(BaseEntityFilter filter)
        {
            return await databaseExecuterService.RunStoredProcedureAsync(GetByFilterProc, (reader) =>
            {
                var filteredList = new List<CandidateRegister>();

                do
                {
                    if (reader.HasRows)
                    {
                        var entity = new CandidateRegister();

                        entity.ParseData(reader);

                        filteredList.Add(entity);
                    }
                } while (reader.Reader.Read()); //we activate the read after as the first one is done in the base DB class
                return filteredList;

            }, GetEntityProperties(filter));
        }
    }
}
