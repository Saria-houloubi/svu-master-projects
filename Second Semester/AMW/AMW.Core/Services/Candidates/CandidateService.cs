using AMW.Core.IServices;
using AMW.Core.Services.Base;
using AMW.Data.Models.Candidates;
using System;
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

        public Candidate GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Candidate> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }


        public async Task<Candidate> InsertOrUpdateAsync(Candidate entity)
        {
            return await databaseExecuterService.RunStoredProcedureAsync<Candidate>(InsertOrUpdateCandiateProc, (reader) =>
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
