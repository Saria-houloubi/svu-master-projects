using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SVU.Logging.IServices;
using SVU.Shared.Messages;

namespace SVU.Web.UI.Controllers.Base
{
    /// <summary>
    /// The base controller for cross properties and functions
    /// </summary>
    public class BaseController : Controller
    {
        #region Properties
        public ILoggingService LoggingService { get; set; }
        #endregion

        #region Constructer
        /// <summary>
        /// Default constructer
        /// </summary>
        public BaseController(ILoggingService loggingService)
        {
            LoggingService = loggingService;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Returns a 500 internal server error with the sent message
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        protected IActionResult InternalServerError(object message = null) => StatusCode(StatusCodes.Status500InternalServerError, 
            new
            {
                message =message ?? ErrorMessages.SomthingWorngHappend
            });
        protected IActionResult CustomBadRequest(object message = null) => StatusCode(StatusCodes.Status400BadRequest,
            new
            {
                message =  message ??  ErrorMessages.InvaildData
            });
        protected IActionResult InvaildLoginAttempt(object message = null) => StatusCode(StatusCodes.Status401Unauthorized, 
            new
            {
                message = message ?? ErrorMessages.InvaildLoginAttempt
            });
        #endregion
    }
}
