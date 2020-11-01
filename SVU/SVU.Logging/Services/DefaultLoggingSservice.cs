using Microsoft.AspNetCore.Http;
using SVU.Logging.IServices;
using System;
using System.Threading.Tasks;

namespace SVU.Logging.Services
{
    /// <summary>
    /// The default loggin service class
    /// </summary>
    public class DefaultLoggingSservice : ILoggingService
    {
        #region Constructer
        /// <summary>
        /// Default constructer
        /// </summary>
        public DefaultLoggingSservice()
        {

        }
        #endregion
        public void LogException(Exception ex)
        {
            Console.WriteLine(ex.GetBaseException().Message);
        }

        public void LogRequest(HttpRequest data)
        {
            Console.WriteLine(data.ToString());
        }
    }
}
