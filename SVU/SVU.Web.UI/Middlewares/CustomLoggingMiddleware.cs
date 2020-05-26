using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using SVU.Logging.IServices;
using SVU.Web.UI.Models.Configuration;
using System.Linq;
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
        public IOptions<LoggingRestrictionOptions> LoggingRestrictionOptions { get; private set; }
        #endregion
        #region Constructer
        /// <summary>
        /// Default constructer
        /// </summary>
        public CustomLoggingMiddleware(RequestDelegate requestDelegate, ILoggingService loggingService, IOptions<LoggingRestrictionOptions> loggingOptions)
        {
            next = requestDelegate;
            LoggingService = loggingService;
            LoggingRestrictionOptions = loggingOptions;
        }
        #endregion

        #region Methods

        public Task Invoke(HttpContext httpContext)
        {
            //Stop logging for some hosts
            if (!LoggingRestrictionOptions.Value.BlockedHosts.Contains(httpContext.Request.Host.Host))
            {
                Task.Run(() => LoggingService.LogRequest(httpContext.Request));
            }

            return next(httpContext);
        }
        #endregion
    }
}
