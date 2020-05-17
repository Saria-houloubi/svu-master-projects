using SVU.Database.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SVU.Database.IService
{
    /// <summary>
    /// The functions to work with the account in the health department
    /// </summary>
    public interface IHealthAccountService
    {
        /// <summary>
        /// Tries to authenticate the user
        ///     if authenticated will return the user
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        Task<HealthUser> AuthenticateUser(string username, string password);

    }
}
