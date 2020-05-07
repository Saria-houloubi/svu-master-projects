using Microsoft.EntityFrameworkCore;
using SVU.Database.DatabaseContext;
using SVU.Database.IService;
using SVU.Database.Models;
using SVU.Database.Service.MSSQL.Base;
using SVU.Logging.IServices;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SVU.Database.Service.MSSQL
{
    /// <summary>
    /// The implementation for <see cref="IDataSetDatabaseService"/>
    /// </summary>
    public class MSSQLDataSetDatabaseService : BaseMSSQLService, IDataSetDatabaseService
    {
        #region Properties

        #endregion

        #region Constructer
        /// <summary>
        /// Default constructer
        /// </summary>
        public MSSQLDataSetDatabaseService(SVUDbContext dbContext, ILogginService logginService)
            : base(dbContext, logginService)
        {

        }
        #endregion

        public async Task<IEnumerable<HeartDisease>> GetHeartDiseaseRecords()
        {
            try
            {
                return await DbContext.HeartDiseases.ToListAsync();
            }
            catch (System.Exception ex)
            {
                LogginService.LogException(ex);
            }
            return null;
        }
    }
}
