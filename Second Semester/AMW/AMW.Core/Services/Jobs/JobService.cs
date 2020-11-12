using AMW.Core.IServices;
using AMW.Core.Services.Base;
using AMW.Data.Models.Jobs;

namespace AMW.Core.Services.Jobs
{
    public partial class JobService : BaseAmwRepositoryService<Job>
    {

        #region Constructer
        /// <summary>
        /// Default constructer
        /// </summary>
        /// <param name="databaseExecuterService"></param>
        public JobService(IDatabaseExecuterService databaseExecuterService)
            : base(databaseExecuterService)
        {
        }
        #endregion
    }
}
