using AMW.Core.IServices;

namespace AMW.Core.Services
{
    public class AmwDefaultAuthService : IAuthService
    {
        /// <summary>
        /// Defauilst to not authorized
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual bool TryLogin(string username, string password, out int id)
        {
            id = -1;
            return false;
        }
    }
}
