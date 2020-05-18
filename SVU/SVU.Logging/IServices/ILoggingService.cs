using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace SVU.Logging.IServices
{
    /// <summary>
    /// The base logging service functions
    /// </summary>
    public interface ILoggingService
    {
        /// <summary>
        /// Logs an exception
        /// </summary>
        /// <param name="ex"></param>
        /// <returns></returns>
        Task LogException(Exception ex);
        /// <summary>
        /// Logs a request data
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        Task LogRequest(HttpRequest data);

    }
}
