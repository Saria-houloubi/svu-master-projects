using Microsoft.AspNetCore.Mvc;
using SVU.Web.UI.Controllers.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SVU.Web.UI.Controllers
{

    /// <summary>
    /// The controller to access the class data
    /// </summary>
    public class ClassController : BaseController
    {
        #region Properties

        #endregion

        #region Constructer
        /// <summary>
        /// Default constructer
        /// </summary>
        public ClassController()
        {

        }
        #endregion

        #region GET Requests
        /// <summary>
        /// Gets the views/class/ade.cshtml
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult ADE()
        {
            return View();
        }
        /// <summary>
        /// Gets the views/class/adm.cshtml
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult ADM()
        {
            return View();
        }
        /// <summary>
        /// Gets the views/class/adp.cshtml
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult ADP()
        {
            return View();
        }
        /// <summary>
        /// Gets the views/class/awe.cshtml
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult AWE()
        {
            return View();
        }
        /// <summary>
        /// Gets the views/class/awp.cshtml
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult AWP()
        {
            return View();
        }

        #endregion

    }
}
