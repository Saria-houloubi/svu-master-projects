using SVU.Database.DatabaseContext;
using SVU.Database.IService;
using SVU.Database.Service.MSSQL.Base;
using SVU.Logging.IServices;
using System;

namespace SVU.Database.Service.MSSQL
{
    /// <summary>
    /// The initializing class for mssql databases for <see cref="IInitializeDatabaseService"/>
    /// </summary>
    public class MSSQLInitializeDatabaseService : BaseMSSQLService, IInitializeDatabaseService
    {
        #region Properties
        public ILogginService LogginService { get; private set; }
        #endregion

        #region Constructer
        /// <summary>
        /// Default constructer
        /// </summary>
        public MSSQLInitializeDatabaseService(ILogginService logginService, SVUDbContext dbContext)
            : base(dbContext)
        {
            LogginService = logginService;
        }
        #endregion

        public bool InitailizeDatabase()
        {
            try
            {
                if (DbContext.Database.EnsureCreated())
                {
                    #region Seed data here

                    #endregion

                    return true;
                }
            }
            catch (Exception ex)
            {
                LogginService.LogException(ex);
            }
            return false;
        }
    }
}
