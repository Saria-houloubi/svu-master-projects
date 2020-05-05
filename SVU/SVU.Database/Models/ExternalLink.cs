using SVU.Database.Models.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace SVU.Database.Models
{

    /// <summary>
    /// Any external links for a course, homework or a program
    /// </summary>
    [Table(name: "ExternalLinks", Schema = "Application")]
    public class ExternalLink : BaseEntityModel
    {
        #region Properties
        public string Description { get; set; }
        public string Note { get; set; }
        public string Link { get; set; }
        #endregion

        #region Constructer
        /// <summary>
        /// Default constructer
        /// </summary>
        public ExternalLink()
        {

        }
        #endregion
    }
}
