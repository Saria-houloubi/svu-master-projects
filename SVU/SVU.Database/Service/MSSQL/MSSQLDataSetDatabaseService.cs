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
        public MSSQLDataSetDatabaseService(SVUDbContext dbContext, ILoggingService logginService)
            : base(dbContext, logginService)
        {

        }

        #endregion

        public async Task<IEnumerable<object>> GetDbSet(string setName)
        {
            try
            {
                switch (setName.ToLower())
                {
                    case "tennis":
                        return await DbContext.Tennis.ToListAsync();
                    case "heartdisease":
                        return await DbContext.HeartDiseases.ToListAsync();
                    default:
                        break;
                }
            }
            catch (System.Exception ex)
            {
                LogginService.LogException(ex);
            }
            return null;
        }
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

        public async Task<IEnumerable<Tennis>> GetTennisRecords()
        {
            try
            {
                return await DbContext.Tennis.ToListAsync();
            }
            catch (System.Exception ex)
            {
                LogginService.LogException(ex);
            }
            return null;
        }
    }
}
