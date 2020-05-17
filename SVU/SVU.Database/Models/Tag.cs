using SVU.Database.Models.Base;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace SVU.Database.Models
{
    /// <summary>
    /// A tag model for anythings
    /// </summary>
    [Table("Tags", Schema = "Application")]
    public class Tag : BaseEntityModel
    {
        #region Properties
        public string Name { get; set; }
        #endregion

        #region Navigation Properties
        public IEnumerable<BlogTag> Blogs { get; set; }
        #endregion

        #region Constructer
        /// <summary>
        /// Defualt constructer
        /// </summary>
        public Tag()
        {

        }
        #endregion
    }
}
