using SVU.Database.Models.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace SVU.Database.Models
{
    /// <summary>
    /// The homeworks for the students
    /// </summary>
    [Table(name: "Homeworks", Schema = "Student")]
    public class Homework : BaseEntityModel
    {
        #region Properties

        public string Description { get; set; }
        public string Note { get; set; }
        public string CodeLink { get; set; }
        public string OnlineLink { get; set; }

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
        public Homework()
        {

        }
        #endregion
    }
}
