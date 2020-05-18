using Microsoft.AspNetCore.Http;
using SVU.Logging.IServices;
using System.Threading.Tasks;

namespace SVU.Web.UI.Middlewares
{
    /// <summary>
    /// The custom middle ware to log all request
    /// </summary>
    public class CustomLoggingMiddleware
    {
        #region Properties
        private readonly RequestDelegate next;
        public ILoggingService LoggingService { get; private set; }
        #endregion
        #region Constructer
        /// <summary>
        /// Default constructer
        /// </summary>
        public CustomLoggingMiddleware(RequestDelegate requestDelegate, ILoggingService loggingService)
        {
            next = requestDelegate;
            LoggingService = loggingService;
        }
        #endregion

        #region Methods

        public Task Invoke(HttpContext httpContext)
        {

            Task.Run(() => LoggingService.LogRequest(httpContext.Request));

            return next(httpContext);
        }
        #endregion
    }
}
