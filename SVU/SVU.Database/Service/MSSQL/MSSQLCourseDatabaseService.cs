using Microsoft.EntityFrameworkCore;
using SVU.Database.DatabaseContext;
using SVU.Database.IService;
using SVU.Database.Models;
using SVU.Database.Service.MSSQL.Base;
using SVU.Logging.IServices;
using System.Threading.Tasks;

namespace SVU.Database.Service.MSSQL
{
    /// <summary>
    /// The mssql implementation for working with <see cref="ICourseDatabaseService"/>
    /// </summary>
    public class MSSQLCourseDatabaseService : BaseMSSQLService, ICourseDatabaseService
    {
        #region Properties
        #endregion

        #region Constructer
        /// <summary>
        /// Default constructer
        /// </summary>
        public MSSQLCourseDatabaseService(SVUDbContext dbContext, ILogginService logginService)
            : base(dbContext,logginService)
        {
        }
        #endregion

        public async Task<Course> GetCourse(string name)
        {
            try
            {
                return await DbContext.Courses
                    .Include(item => item.Links)
                    .Include(item => item.Sessions).ThenInclude(item=>item.Links)
                    .Include(item => item.Homeworks).ThenInclude(item => item.Links)
                    .SingleOrDefaultAsync(item => item.ShortName.ToLower() == name.ToLower());
            }
            catch (System.Exception ex)
            {
                LogginService.LogException(ex);
            }
            return null;
        }
    }
}
