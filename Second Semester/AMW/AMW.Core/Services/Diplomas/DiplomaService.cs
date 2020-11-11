using AMW.Core.IServices;
using AMW.Core.Services.Base;
using AMW.Data.Models.Diplomas;

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
    }
}
