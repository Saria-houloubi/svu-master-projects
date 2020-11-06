using log4net;
using Microsoft.AspNetCore.Mvc;

namespace AMW.API.Controllers.Base
{

    /// <summary>
    /// Base controller for cross functions and data
    /// </summary>
    [Route("api/[controller]")]
    public abstract partial class BaseController : Controller
    {

        #region Properties
        public ILog log { get; protected set; }
        #endregion
        #region Constructer
        /// <summary>
        /// Default constroller
        /// </summary>
        public BaseController()
        {
            log = LogManager.GetLogger(this.GetType().Name);
        }
        #endregion
    }
}
