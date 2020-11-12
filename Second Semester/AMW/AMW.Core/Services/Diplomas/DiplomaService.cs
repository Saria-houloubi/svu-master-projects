using AMW.Core.IServices;
using AMW.Core.Services.Base;
using AMW.Data.Models.Diplomas;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AMW.Core.Services.Diplomas
{
    public partial class DiplomaService : BaseAmwRepositoryService<Diploma>
    {
        #region Constructer
        /// <summary>
        /// Default constructer
        /// </summary>
        public DiplomaService(IDatabaseExecuterService databaseExecuterService)
            : base(databaseExecuterService)
        {

        }
        #endregion


        public override async Task<bool> DeleteAsync(Diploma entity)
        {
            var recordsAffected = await databaseExecuterService.RunStoredProcedureAsync(DeleteEntityProc, new Dictionary<string, object>()
            {
                {"id" ,entity.Id },
                {"candidateId",entity.CandidateId }
            });

            return recordsAffected > 0;
        }
    }
}
