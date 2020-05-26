using BotDetect.Web.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using SVU.Shared.Messages;
using SVU.Web.UI.Models.Captcha;
using SVU.Web.UI.ViewModels.Base;

namespace SVU.Web.UI.Attribute
{
    /// <summary>
    /// Our wrapper attribute for easy user
    /// </summary>
    public class BotDetectorCaptchaAttribute : ActionFilterAttribute
    {
        #region Properties
        public string CaptchaId { get; set; }
        public string ErrorMessage { get; set; }
        public string InputField { get; set; }

        #endregion

        #region Methods
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //A falg if the user captcha code is correc
            bool validCaptcha = false;
            //Try to get the model from the request
            if (filterContext.ActionArguments != null && filterContext.ActionArguments.TryGetValue("model", out object data))
            {
                //If the data is the same type or derived from baseviewmodel
                if (data is BaseViewModel)
                {
                    if (((dynamic)data).Captcha is CaptchaModel captcha)
                    {
                        MvcCaptcha mvcCaptcha = new MvcCaptcha(CaptchaId);

                        if (mvcCaptcha.Validate(captcha.CaptchaCode))
                        {
                            MvcCaptcha.ResetCaptcha(CaptchaId);

                            validCaptcha = true;

                        }
                    }
                }
            }

            //If not vaild then add the error to the model state
            if (!validCaptcha)
            {
                filterContext.ModelState.TryAddModelError("Capatcha", ErrorMessage);
            }

            base.OnActionExecuting(filterContext);
        }
        #endregion
        #region Constructer
        /// <summary>
        /// Default constructer
        /// </summary>
        public BotDetectorCaptchaAttribute()
            : this("BotDetectorCaptcha", "CaptchaCode", ErrorMessages.WrongCaptcha)
        {

        }
        public BotDetectorCaptchaAttribute(string captchaId, string inputField, string errorMessage = null)
        {
            CaptchaId = captchaId;
            InputField = inputField;
            ErrorMessage = errorMessage ?? ErrorMessages.WrongCaptcha;
        }
        #endregion
    }
}
