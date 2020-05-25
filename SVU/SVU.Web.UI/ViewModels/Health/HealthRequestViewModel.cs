using SVU.Web.UI.ViewModels.Base;

namespace SVU.Web.UI.ViewModels.Health
{
    public class HealthRequestViewModel : BaseViewModel
    {
        #region Properties
        public string Content { get; set; }

        #endregion

        #region Constructer
        /// <summary>
        /// Default constructer
        /// </summary>
        public HealthRequestViewModel()
        {
        }
        #endregion
    }
}
