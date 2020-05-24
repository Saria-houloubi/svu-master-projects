using Microsoft.EntityFrameworkCore;
using SVU.Database.DatabaseContext;
using SVU.Database.IService;
using SVU.Database.Models;
using SVU.Database.Service.MSSQL.Base;
using SVU.Logging.IServices;
using SVU.Shared.Extensions;
using System.Threading.Tasks;

namespace SVU.Database.Service.MSSQL
{
    /// <summary>
    /// The msSQL implementation for <see cref="IHealthAccountService"/>
    /// </summary>
    public class MSSQLHealthAccountService : BaseMSSQLService, IHealthAccountService
    {
        #region Properties

        #endregion

        #region Constructer
        /// <summary>
        /// Defualt constructer
        /// </summary>
        public MSSQLHealthAccountService(SVUDbContext dbContext, ILoggingService loggingService)
            : base(dbContext, loggingService)
        {

        }
        #endregion

        public async Task<HealthUser> AuthenticateUser(string username, string password)
        {
            //Get the user from the database
            var user = await DbContext.HealthUsers
                .Include(item => item.Role)
                .SingleOrDefaultAsync(item => item.Username == username);
            //Check that there is data with this user
            if (user != null)
            {
                //Check if the pasword is matches
                if (password.VerifyMD5Hash(user.Password))
                {
                    return user;
                }
            }

            return null;
        }

        public async Task<bool> VerifyEmail(string email)
        {
            return await DbContext.HealthUsers.AnyAsync(item => item.Email != email);
        }

        public async Task<bool> VerifyUsername(string username)
        {
            return await DbContext.HealthUsers.AnyAsync(item => item.Username != username);
        }
    }
}
