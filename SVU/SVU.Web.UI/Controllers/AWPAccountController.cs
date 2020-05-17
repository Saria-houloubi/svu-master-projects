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
                    var claimIdentity = new ClaimsIdentity(claims);
                    //Authorize the user and issue a cookie
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimIdentity));
                    return View(returnUrl ?? "/Homework/health.cshtml");
                }
                //Add the error message to the model
                model.Erros.Add(ErrorMessages.InvaildLoginAttempt);
                return View("/homework/health.cshtml", model);
            }
            return CustomBadRequest();
        }
        #endregion
    }
}
