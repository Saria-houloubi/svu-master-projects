﻿using SVU.Database.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace SVU.Web.UI.ViewModels
{
    public class HomeworkHeartDiseaseViewModel
    {

        #region Properties
        public IEnumerable<HeartDisease> HeartDiseasesRecords { get; set; }
        #endregion

        #region Constructer
        /// <summary>
        /// Default constructer
        /// </summary>
        public HomeworkHeartDiseaseViewModel()
        {
            HeartDiseasesRecords = new Collection<HeartDisease>();
        }
        #endregion
    }
}
