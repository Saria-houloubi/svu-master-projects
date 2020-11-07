using AMW.Core.IServices;
using AMW.Core.Services.Base;
using AMW.Data.Models.Base;
using AMW.Data.Models.Candidates;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AMW.Core.Services.Candidates
{
    public partial class CandidateService : BaseAmwRepositoryService, IRepositoryService<Candidate>
    {

        #region Properties
        private readonly IDatabaseExecuterService databaseExecuterService;
        #endregion

        #region Constructer
        public CandidateService(IDatabaseExecuterService databaseExecuterService)
        {
            this.databaseExecuterService = databaseExecuterService;
        }
        #endregion

        public async Task<Candidate> GetByIdAsync(int id)
        {
            return await databaseExecuterService.RunStoredProcedureAsync(GetByIdProc, (reader) =>
            {
                return new Candidate(reader);
            }, new Dictionary<string, object>()
            {
                {nameof(id),id }
            });
        }

        public async Task<IEnumerable<Candidate>> GetByFilterAsync(BaseEntityFilter filter)
        {
            return await databaseExecuterService.RunStoredProcedureAsync(GetByFilterProc, (reader) =>
            {
                var filteredList = new List<Candidate>();

                do
                {
                    if (reader.HasRows)
                    {
                        filteredList.Add(new Candidate(reader));
                    }
                } while (reader.Reader.Read()); //we activate the read after as the first one is done in the base DB class

                return filteredList;

            }, GetEntityProperties(filter));
        }

        public async Task<Candidate> InsertOrUpdateAsync(Candidate entity)
        {
            return await databaseExecuterService.RunStoredProcedureAsync(InsertOrUpdateCandiateProc, (reader) =>
              {
                  return new Candidate(reader);
              }, GetEntityProperties(entity));
        }

        public async Task<IEnumerable<Candidate>> InsertOrUpdateAsync(IEnumerable<Candidate> entities)
        {
            var candidates = new List<Candidate>();

            foreach (var item in entities)
            {
                candidates.Add(await InsertOrUpdateAsync(item));
            }

            return candidates;
        }
    }
}
