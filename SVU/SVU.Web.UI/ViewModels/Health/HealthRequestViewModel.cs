using SVU.Web.UI.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SVU.Web.UI.ViewModels.Health
{
    public class HealthRequestViewModel : BaseViewModel
    {
        #region Properties
        public string RequestSubject { get; set; }
        public int MyProperty { get; set; }
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
