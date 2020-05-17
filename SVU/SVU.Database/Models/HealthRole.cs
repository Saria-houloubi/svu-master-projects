using SVU.Database.Models.Base;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace SVU.Database.Models
{
    /// <summary>
    /// The roles that a health user can be granted to
    /// </summary>
    [Table("Roles", Schema = "Health")]
    public class HealthRole : BaseEntityModel
    {
        #region Properties

        public string Name { get; set; }
        #endregion

        #region Navigation Properties

        /// <summary>
        /// The users that hold this role
        /// </summary>
        public IEnumerable<HealthUser> Users { get; set; }
        #endregion

        #region Constructer
        /// <summary>
        /// Default constructer
        /// </summary>
        public HealthRole()
        {

        }
        #endregion
    }
}
