using SVU.Database.Models;
using System;
using System.Threading.Tasks;

namespace SVU.Database.IService
{
    /// <summary>
    /// The functions to work with the account in the health department
    /// </summary>
    public interface IHealthAccountService
    {


        /// <summary>
        /// Gets a user by his/here id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<HealthUser> GetUser(Guid id);

        /// <summary>
        /// Adds or updates user information 
        /// </summary>
        /// <param name="user"></param>
        /// <param name="roleName"></param>
        /// <returns></returns>
        Task<HealthUser> AddOrUpdateUser(HealthUser user, string roleName);

        /// <summary>
        /// Tries to authenticate the user
        ///     if authenticated will return the user
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        Task<HealthUser> AuthenticateUser(string username, string password);

        /// <summary>
        /// Checks if the username is in use
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        Task<bool> VerifyUsername(string username);
        Task<bool> VerifyEmail(string email);
    }
}
