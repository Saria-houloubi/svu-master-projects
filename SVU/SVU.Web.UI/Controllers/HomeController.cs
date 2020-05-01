using Microsoft.AspNetCore.Mvc;
using SVU.Web.UI.Controllers.Base;

namespace SVU.Web.UI.Controllers
{
    public class HomeController : BaseController
    {

        #region Properties

        #endregion

        #region Constructer
        /// <summary>
        /// Default constructer
        /// </summary>
        public HomeController()
        {

        }
        #endregion


        #region GET Requests
        /// <summary>
        /// Gets the Index page Views/Home/index.cshtml
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            return View();
        }
        #endregion
    }
}
