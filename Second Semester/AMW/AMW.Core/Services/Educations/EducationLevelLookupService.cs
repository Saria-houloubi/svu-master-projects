using AMW.Core.IServices;
using AMW.Core.Services.Base;
using AMW.Data.Models.Educations;

namespace AMW.Core.Services.Educations
{
    public partial class EducationLevelLookupService : BaseAmwLookupRepositoryService<EducationLevel>
    {
        #region Constructer
        /// <summary>
        /// Default constructer
        /// </summary>
        /// <param name="databaseExecuterService"></param>
        public EducationLevelLookupService(IDatabaseExecuterService databaseExecuterService)
            : base(databaseExecuterService)
        {
        }
        #endregion
    }
}
