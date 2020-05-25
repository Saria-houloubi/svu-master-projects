using Microsoft.EntityFrameworkCore;
using SVU.Database.DatabaseContext;
using SVU.Database.IService;
using SVU.Database.Models;
using SVU.Database.Service.MSSQL.Base;
using SVU.Logging.IServices;
using SVU.Shared.Extensions;
using System;
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

        public async Task<HealthUser> GetUser(Guid id)
        {
            try
            {
                return await DbContext.HealthUsers
                            .SingleOrDefaultAsync(item => item.Id == id);
                            
            }
            catch (Exception ex)
            {
                LogginService.LogException(ex);
            }

            return null;
        }

        public async Task<HealthUser> AddOrUpdateUser(HealthUser user, string roleName)
        {
            try
            {
                //Get the role
                var role = await DbContext.HealthRoles.SingleOrDefaultAsync(item => item.Name == roleName);

                //Check if the role is vaild and if the user is new
                if (user.Id == Guid.Empty)
                {
                    //Assign the role
                    user.Role = role;
                    //Hash the password
                    user.Password = user.Password.GetMD5Hash();
                    //change the email to lower case
                    user.Email = user.Email.ToLower();
                    //Attach the enitity to the context
                    DbContext.Attach(user);
                    //Mark it as added
                    DbContext.Entry(user).State = EntityState.Added;
                }
                else
                {
                    //Get the user from db
                    var userDb = await DbContext.HealthUsers.SingleOrDefaultAsync(item => item.Id == user.Id);
                    //Update the values
                    userDb.Password = string.IsNullOrEmpty(user.Password) ? userDb.Password : user.Password.GetMD5Hash();
                    userDb.PhoneNumber = user.PhoneNumber;
                    userDb.Gender = user.Gender;
                    userDb.DOB = user.DOB;
                    userDb.MedicalHistory = user.MedicalHistory;
                    userDb.Note = user.Note;
                    //Check if the role was changed
                    if (role != null)
                    {
                        userDb.Role = role;
                    }
                    //Markt the entity as modified
                    DbContext.Entry(userDb).State = EntityState.Modified;
                }

                await DbContext.SaveChangesAsync();

                return user;
            }
            catch (System.Exception ex)
            {
                LogginService.LogException(ex);
            }
            return null;
        }
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
            return !await DbContext.HealthUsers.AnyAsync(item => item.Email == email.ToLower());
        }

        public async Task<bool> VerifyUsername(string username)
        {
            return !await DbContext.HealthUsers.AnyAsync(item => item.Username == username);
        }
    }
}
