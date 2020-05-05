using SVU.Database.DatabaseContext;

namespace SVU.Database.Service.MSSQL.Base
{
    /// <summary>
    /// A base class for cross data and functions
    /// </summary>
    public class BaseMSSQLService
    {
        #region Properties
        protected SVUDbContext DbContext { get; private set; }
        #endregion

        #region Constructer
        /// <summary>
        /// Default constructer
        /// </summary>
        public BaseMSSQLService(SVUDbContext dbContext)
        {
            DbContext = dbContext;
        }
        #endregion
    }
}
