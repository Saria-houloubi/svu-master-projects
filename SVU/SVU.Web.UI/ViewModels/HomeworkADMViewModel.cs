using SVU.Database.Models;
using System.Collections.Generic;

namespace SVU.Web.UI.ViewModels
{
    public class HomeworkADMViewModel
    {

        #region Properties
        public IEnumerable<HeartDisease> HeartDiseasesRecords { get; set; }
        #endregion

        #region Constructer
        /// <summary>
        /// Default constructer
        /// </summary>
        public HomeworkADMViewModel()
        {

        }
        #endregion
    }
}
