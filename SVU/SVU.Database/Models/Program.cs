using SVU.Database.Models.Base;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace SVU.Database.Models
{
    /// <summary>
    /// The programs for the students is inrolled in 
    /// </summary>
    [Table(name: "Programs", Schema = "SVU")]
    public class Program : BaseEntityModel
    {
        #region Properties
        public string Name { get; set; }
        #endregion

        #region Navigation Properties
        public IEnumerable<Course> Courses { get; set; }
        public IEnumerable<ExternalLink> Links { get; set; }

        #endregion

        #region Constructer
        /// <summary>
        /// Default constructer
        /// </summary>
        public Program()
        {
            //Create the collection 
            Courses = new Collection<Course>();
            Links = new Collection<ExternalLink>();

        }
        #endregion
    }
}
