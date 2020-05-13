using SVU.Database.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace SVU.Web.UI.ViewModels
{
    public class HomeworkTennisViewModel
    {

        #region Properties
        public IEnumerable<Tennis> TennisRecords { get; set; }
        #endregion

        #region Constructer
        /// <summary>
        /// Default constructer
        /// </summary>
        public HomeworkTennisViewModel()
        {

            TennisRecords = new Collection<Tennis>();
        }
        #endregion
    }
}
