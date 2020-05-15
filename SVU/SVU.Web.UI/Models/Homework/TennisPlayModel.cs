using System.ComponentModel.DataAnnotations;

namespace SVU.Web.UI.Models.Homework
{
    /// <summary>
    /// The model for a Tennis play
    /// </summary>
    public class TennisPlayModel
    {
        #region Properties
        [Required]
        public string Outlook { get; set; }
        [Required]
        public string Humidity { get; set; }
        [Required]
        public string Temperature { get; set; }
        [Required]
        public string Wind { get; set; }
        #endregion

        #region Constructer
        /// <summary>
        /// Default constructer
        /// </summary>
        public TennisPlayModel()
        {

        }
        #endregion
    }
}
