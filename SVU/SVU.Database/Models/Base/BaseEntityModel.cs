using System;
using System.ComponentModel.DataAnnotations;

namespace SVU.Database.Models.Base
{
    /// <summary>
    /// The base model for database entities
    /// </summary>
    public class BaseEntityModel
    {
        #region Properties
        [Key]
        public Guid Id { get; set; }
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
        public BaseEntityModel()
        {

        }
        #endregion
    }
}
