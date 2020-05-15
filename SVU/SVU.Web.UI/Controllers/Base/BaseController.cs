using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SVU.Web.UI.Controllers.Base
{
    /// <summary>
    /// The base controller for cross properties and functions
    /// </summary>
    public class BaseController : Controller
    {
        #region Properties

        #endregion

        #region Constructer
        /// <summary>
        /// Default constructer
        /// </summary>
        public BaseController()
        {

        }
        #endregion

        #region Methods
        /// <summary>
        /// Returns a 500 internal server error with the sent message
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        protected IActionResult InternalServerError(object message = null) => StatusCode(StatusCodes.Status500InternalServerError, message);
        #endregion
    }
}
