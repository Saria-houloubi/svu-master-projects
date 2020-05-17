using SVU.Web.UI.ViewModels.Base;
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

