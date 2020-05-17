using SVU.Database.Models.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SVU.Database.Models
{
    [Table("BlogTags",Schema ="Application")]
    public class BlogTag : BaseEntityModel
    {
        #region Properties

        [ForeignKey(nameof(Blog))]
        public Guid BlogId{ get; set; }
        [ForeignKey(nameof(Tag))]
        public Guid TagId { get; set; }
        #endregion

        #region Navigation Properties
        public Blog Blog { get; set; }
        public Tag Tag{ get; set; }
        #endregion


        #region Constructer
        /// <summary>
        /// Default constructer
        /// </summary>
        public BlogTag()
        {

        }
        #endregion
    }
}
