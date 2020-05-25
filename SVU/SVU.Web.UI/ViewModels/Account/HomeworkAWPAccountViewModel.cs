using SVU.Database.Models;
using SVU.Web.UI.ViewModels.Base;
using SVU.Web.UI.ViewModels.Health;
using System.ComponentModel.DataAnnotations;

namespace SVU.Web.UI.ViewModels.Account
{
    /// <summary>
    /// The model to get authorized for AWP homework
    /// </summary>
    public class HomeworkAWPAccountViewModel : BaseViewModel
    {
        #region Properties
        [Required]
        public string Usernmae { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public HealthUserViewModel UserViewModel { get; set; }
        #endregion

        #region Constructer
        /// <summary>
        /// Default constructer
        /// </summary>
        public HomeworkAWPAccountViewModel()
        {

        }
        #endregion
    }
}

