using SVU.Database.Models.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SVU.Database.Models
{
    /// <summary>
    /// The model that represents any blog posted in this applicaiton
    /// </summary>
    [Table("Blogs", Schema = "Application")]
    public class Blog : BaseEntityModel
    {
        #region Properties
        [Required]
        public string Title { get; set; }
        /// <summary>
        /// The image will be saved as a base64 string
        /// </summary>
        public string Thumbnail { get; set; }
        /// <summary>
        /// The content of the image which will be in html or .md type
        /// </summary>
        [Required]
        public string Content { get; set; }

        public int VisitCout { get; set; }

        [ForeignKey(nameof(LastEditUser))]
        public Guid LastEditUserId { get; set; }
        #endregion

        #region Navigation Properties
        /// <summary>
        /// The auther of this artical
        /// </summary>
        public HealthUser Auther { get; set; }
        public HealthUser LastEditUser { get; set; }

        public IEnumerable<BlogTag> Tags { get; set; }
        #endregion

        #region Constructer
        /// <summary>
        /// Default constructer
        /// </summary>
        public Blog()
        {

        }
        #endregion
    }
}
