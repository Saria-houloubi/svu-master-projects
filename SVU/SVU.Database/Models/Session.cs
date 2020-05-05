using SVU.Database.Models.Base;
using System;
using System.Collections.Generic;
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
        public string Description { get; set; }
        public float FileSize { get; set; }
        public string DownloadLink { get; set; }
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

        }
        #endregion
    }
}
