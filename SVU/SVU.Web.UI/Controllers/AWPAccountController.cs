using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using SVU.Database.IService;
using SVU.Logging.IServices;
using SVU.Shared.Messages;
using SVU.Web.UI.Controllers.Base;
using SVU.Web.UI.ViewModels.Account;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SVU.Web.UI.Controllers
{
    /// <summary>
    /// The Account Auth controller for AWP homeworks
    /// </summary>
    public class AWPAccountController : BaseController
    {
        #region Properties
        public IHealthAccountService HealthAccountService { get; private set; }
        #endregion

        #region Constructer
        /// <summary>
        /// Default constructer
        /// </summary>
        public AWPAccountController(ILoggingService loggingService, IHealthAccountService healthAccountService)
            : base(loggingService)
        {
            HealthAccountService = healthAccountService;
        }
        #endregion

        #region GET Requests
        /// <summary>
        /// Checks if the username is in use
        /// </summary>
        /// <param name="username">The usename to verify for</param>
        /// <returns></returns>
        [AcceptVerbs("GET", "POST")]
        public async Task<IActionResult> VerifyUsername(string username)
        {
            //Check if we got any data
            if (!string.IsNullOrEmpty(username) && await HealthAccountService.VerifyUsername(username))
            {
                return Json(true);
            }
            return Json(ErrorMessages.UsernameIsUsed(username));
        }
        /// <summary>
        /// Checks if the email is in use
        /// </summary>
        /// <param name="email">The email to verify for</param>
        /// <returns></returns>
        [AcceptVerbs("GET", "POST")]
        public async Task<IActionResult> VerifyEmail(string email)
        {
            //Check if we got any data
            if (!string.IsNullOrEmpty(email))
            {
                return Ok(await HealthAccountService.VerifyEmail(email));
            }
            return Ok(ErrorMessages.EmailIsUsed(email));
        }

        #endregion
        #region POST Requests
        /// <summary>
        /// Trys to logs the user in
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Login(string returnUrl, [FromForm] HomeworkAWPAccountViewModel model)
        {
            if (ModelState.IsValid)
            {
                //Check the sent data
                var result = await HealthAccountService.AuthenticateUser(model.Usernmae, model.Password);

                if (result != null)
                {
                    //Create the claims that will be stored in the cookie
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name , result.Username),
                        new Claim(ClaimTypes.Role , result.Role.Name),
                        new Claim(ClaimTypes.NameIdentifier, result.Id.ToString()),
                    };
                    //Create the claim identity
                    var claimIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    //Authorize the user and issue a cookie
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimIdentity));
                    return Redirect(returnUrl ?? "/homework/awp");
                }
                //Add the error message to the model
                model.Errors.Add(ErrorMessages.InvaildLoginAttempt);
                return View("/Views/homework/health.cshtml", model);
            }
            return CustomBadRequest();
        }
        #endregion
    }
}
