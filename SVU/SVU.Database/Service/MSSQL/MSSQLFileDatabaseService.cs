using Microsoft.EntityFrameworkCore;
using SVU.Database.DatabaseContext;
using SVU.Database.IService;
using SVU.Database.Models;
using SVU.Database.Service.MSSQL.Base;
using SVU.Logging.IServices;
using System;
using System.Threading.Tasks;

namespace SVU.Database.Service.MSSQL
{
    /// <summary>
    /// The mssql implementation for <see cref="IFileDatabaseService"/>
    /// </summary>
    public class MSSQLFileDatabaseService : BaseMSSQLService, IFileDatabaseService
    {
        #region Properties

        #endregion

        #region Constructer
        /// <summary>
        /// Default constructer
        /// </summary>
        public MSSQLFileDatabaseService(SVUDbContext dbContext, ILogginService logginService)
            : base(dbContext, logginService)
        {
        }
        #endregion

        public async Task<ExternalLink> GetLinkInfo(Guid id)
        {
            try
            {
                return await DbContext.ExternalLinks.SingleOrDefaultAsync(item => item.Id == id);
            }
            catch (Exception ex)
            {
                LogginService.LogException(ex);
            }
            return null;
        }
    }
}
