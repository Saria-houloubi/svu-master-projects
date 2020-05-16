using SVU.Database.Models.Base;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace SVU.Database.Models
{
    [Table("UserRoles",Schema ="Health")]
    public class HealthUserRole : BaseEntityModel
    {
        #region Properties

        [ForeignKey(nameof(User))]
        public Guid UserId{ get; set; }
        [ForeignKey(nameof(Role))]
        public Guid RoleId { get; set; }
        #endregion

        #region Navigation Properties
        public HealthUser User{ get; set; }
        public HealthRole Role{ get; set; }
        #endregion


        #region Constructer
        /// <summary>
        /// Default constructer
        /// </summary>
        public HealthUserRole()
        {

        }
        #endregion
    }
}
