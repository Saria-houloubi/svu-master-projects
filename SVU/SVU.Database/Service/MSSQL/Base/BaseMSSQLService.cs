using SVU.Database.DatabaseContext;
using SVU.Logging.IServices;

namespace SVU.Database.Service.MSSQL.Base
{
    /// <summary>
    /// A base class for cross data and functions
    /// </summary>
    public class BaseMSSQLService
    {
        #region Properties
        public ILoggingService LogginService { get; private set; }

        protected SVUDbContext DbContext { get; private set; }
        #endregion

        #region Constructer
        /// <summary>
        /// Default constructer
        /// </summary>
        public BaseMSSQLService(SVUDbContext dbContext,ILoggingService logginService)
        {
            DbContext = dbContext;
            LogginService = logginService;
        }
        #endregion
    }
}
