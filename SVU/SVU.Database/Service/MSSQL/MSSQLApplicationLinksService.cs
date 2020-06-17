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
    /// The implementation for <see cref="IApplicationLinksService"/>
    /// </summary>
    public class MSSQLApplicationLinksService : BaseMSSQLService, IApplicationLinksService
    {
        #region Constructer
        /// <summary>
        /// Default constructer
        /// </summary>
        public MSSQLApplicationLinksService(SVUDbContext dbContext, ILoggingService loggingService)
            : base(dbContext, loggingService)
        {

        }
        #endregion
        public async Task<ExternalLink> GetLink(Guid id)
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
