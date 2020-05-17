using SVU.Database.Models.Base;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace SVU.Database.Models
{
    /// <summary>
    /// The model represting a user for the health part of the project
    /// </summary>
    [Table("Users", Schema = "Health")]
    public class HealthUser : BaseEntityModel
    {
        #region Properties
        public string Username { get; set; }
        public string Password { get; set; }
        #endregion

        #region Navigation Properties
        /// <summary>
        /// The roles this use has been granted
        /// </summary>
        public IEnumerable<HealthUserRole> Roles { get; set; }
        #endregion
        #region Constructer
        /// <summary>
        /// Default constructer
        /// </summary>
        public HealthUser()
        {

        }
        #endregion
    }
}
