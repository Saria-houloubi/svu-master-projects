using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using SVU.Database.IService;
using SVU.Logging.IServices;
using SVU.Shared.Messages;
using SVU.Shared.Static;
using SVU.Web.UI.Attribute;
using SVU.Web.UI.Controllers.Base;
using SVU.Web.UI.Extensions;
using SVU.Web.UI.Static;
using SVU.Web.UI.ViewModels.Account;
using SVU.Web.UI.ViewModels.Health;
using System;
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
        /// <summary>
        /// Gets a page showing that the user dose not have access to do the operation
        /// </summary>
        /// <param name="returnUrl"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult AccessDenied(string returnUrl)
        {
            return View(StaticViewNames.ACCESSDENIED);
        }
        #endregion

        #region POST Requests
        /// <summary>
        /// Trys to logs the user in
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [BotDetectorCaptcha]
        public async Task<IActionResult> Login(string returnUrl, [FromForm] HomeworkAWPAccountViewModel model)
        {
            if (ModelState.IsValid)
            {
                //Check the sent data
                var result = await HealthAccountService.AuthenticateUser(model.Username, model.Password);

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
            }
            else
            {
                model.Errors.AddRange(ModelState.GetValidationErrors());
            }
            //Reset captch value
            model.Captcha.CaptchaCode = string.Empty;

            return View(StaticViewNames.AWP_HEALTH, model);
        }

        /// <summary>
        /// Registers a new basic users
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [BotDetectorCaptcha]
        public async Task<IActionResult> AddOrUpdateUser(HealthUserViewModel model)
        {
            //Check if the model is valid
            if (ModelState.IsValid ||
                //This one is when an update happens so the erros are for passwords , username and email that we can skip
                (model.Id != Guid.Empty && ModelState.ErrorCount == 4))
            {
                try
                {
                    //try to add the user
                    var user = await HealthAccountService.AddOrUpdateUser(new Database.Models.HealthUser
                    {
                        Id = model.Id,
                        Username = model.Username,
                        Email = model.Email,
                        PhoneNumber = model.PhoneNumber,
                        Gender = model.Gender,
                        MedicalHistory = model.MedicalHistory,
                        Password = model.Passoword,
                        DOB = model.DOB,
                    }, UserRoles.BASIC);
                    //Check if we got the data right
                    if (user != null)
                    {
                        //Assign the id back
                        model.Id = user.Id;
                        //Clear the password before sending it back
                        model.Passoword = string.Empty;
                        model.PasswordConfirmation = string.Empty;

                        return RedirectToAction("awp", "homework");
                    }
                    else
                    {
                        model.Errors.Add(ErrorMessages.SomthingWorngHappend);
                    }

                }
                catch (System.Exception ex)
                {
                    LoggingService.LogException(ex);

                    model.Errors.Add(ex.Message);
                }
            }
            else
            {
                model.Errors.AddRange(ModelState.GetValidationErrors());
            }

            return View(StaticViewNames.AWP_HEALTH, new HomeworkAWPAccountViewModel()
            {
                UserViewModel = model
            });
        }
        #endregion
    }
}
