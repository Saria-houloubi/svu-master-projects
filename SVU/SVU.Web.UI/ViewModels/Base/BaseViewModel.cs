using System.Collections.Generic;

namespace SVU.Web.UI.ViewModels.Base
{
    /// <summary>
    /// A base viewmodel for cross functions and data
    /// </summary>
    public class BaseViewModel
    {
        #region Properties
        public List<string> Erros { get; set; }
        #endregion

        #region Constructer
        /// <summary>
        /// Default constructer
        /// </summary>
        public BaseViewModel()
        {
            Erros = new List<string>();
        }
        #endregion

    }
}
