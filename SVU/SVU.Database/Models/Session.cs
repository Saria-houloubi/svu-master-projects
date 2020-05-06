using SVU.Database.Models.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace SVU.Database.Models
{
    /// <summary>
    /// The sessions for a <see cref="Course"/>
    /// </summary>
    [Table(name: "Sessions", Schema = "SVU")]
    public class Session : BaseEntityModel
    {
        #region Properties
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        [ForeignKey(nameof(Course))]
        public Guid CourseId { get; set; }
        #endregion

        #region Navigation Properties
        public Course Course { get; set; }
        public IEnumerable<ExternalLink> Links { get; set; }

        #endregion

        #region Constructer
        /// <summary>
        /// Default constructer
        /// </summary>
        public Session()
        {
            Links = new Collection<ExternalLink>();

        }
        #endregion
    }
}
