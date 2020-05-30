using SVU.Web.UI.ViewModels.Base;
using System;
using System.ComponentModel.DataAnnotations;

namespace SVU.Web.UI.ViewModels.Health
{
    public class HealthRequestViewModel : BaseViewModel
    {
        #region Properties
        public Guid Id { get; set; }
        [Required]
        [StringLength(40)]
        public string Subject { get; set; }
        [Required]
        [StringLength(400)]
        public string Content { get; set; }
        [StringLength(200)]
        public string Note { get; set; }
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
