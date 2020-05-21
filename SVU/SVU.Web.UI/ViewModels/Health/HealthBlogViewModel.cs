using SVU.Database.Models;
using SVU.Web.UI.ViewModels.Base;

namespace SVU.Web.UI.ViewModels.Health
{
    public class HealthBlogViewModel : BaseViewModel
    {
        #region Properties
        public Blog Blog{ get; set; }
        #endregion

        #region Constructer
        /// <summary>
        /// Default constructer
        /// </summary>
        public HealthBlogViewModel()
        {

        }
        #endregion
    }
}
