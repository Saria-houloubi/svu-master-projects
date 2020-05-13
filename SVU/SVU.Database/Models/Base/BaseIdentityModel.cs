using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SVU.Database.Models.Base
{
    /// <summary>
    /// The base model for database entities the key is an int
    /// </summary>
    public class BaseIdentityModel
    {
        #region Properties
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [DataType("datetime2")]
        public DateTime CreationDate { get; set; }
        [DataType("datetime2")]
        public DateTime LastUpdatedDate { get; set; }
        public string Note { get; set; }
        #endregion

        #region Constructer
        /// <summary>
        /// Default constructer
        /// </summary>
        public BaseIdentityModel()
        {

        }
        #endregion
    }
}
