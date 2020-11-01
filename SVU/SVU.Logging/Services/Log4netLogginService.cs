using log4net;
using Microsoft.AspNetCore.Http;
using SVU.Logging.IServices;
using System;

namespace SVU.Logging.Services
{
    public class Log4netLogginService : ILoggingService
    {
        #region Properties
        private static ILog log = LogManager.GetLogger("SVU");
        #endregion
        public void LogException(Exception ex)
        {
            log.Error(ex.Message, ex);
        }

        public void LogRequest(HttpRequest data)
        {
            log.Info($"Request path {data.Path} query {data.QueryString}");
        }
    }
}
