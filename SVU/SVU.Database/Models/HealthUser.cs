﻿using SVU.Database.Models.Base;
using System.ComponentModel.DataAnnotations;
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
        [Required]
        [StringLength(20)]
        public string Username { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        #endregion

        #region Navigation Properties
        /// <summary>
        /// The roles this use has been granted
        /// </summary>
        public HealthRole Role { get; set; }
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
