using SVU.Database.Models.Base;
using SVU.Shared.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace SVU.Database.Models
{
    /// <summary>
    /// The model for a health request by a client
    /// </summary>
    [Table("Requests", Schema = "Health")]
    public class HealthRequest : BaseEntityModel
    {
        #region Properties
        public string Name { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public int Age { get; set; }
        public Gender Gender { get; set; }
        public string MedicalHistory { get; set; }
        public string Content { get; set; }

        #endregion

        #region Navigation Properties

        #endregion

        #region Constructer
        /// <summary>
        /// Default constructer
        /// </summary>
        public HealthRequest()
        {

        }
        #endregion
    }
}
