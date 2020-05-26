using SVU.Web.UI.Models.Captcha;
using System.Collections.Generic;

namespace SVU.Web.UI.ViewModels.Base
{
    /// <summary>
    /// A base viewmodel for cross functions and data
    /// </summary>
    public abstract class BaseViewModel
    {
        #region Properties
        public List<string> Errors { get; set; }
        public CaptchaModel Captcha { get; set; }
        #endregion

        #region Constructer
        /// <summary>
        /// Default constructer
        /// </summary>
        public BaseViewModel()
        {
            Errors = new List<string>();
            Captcha = new CaptchaModel();
        }
        #endregion

    }
}
