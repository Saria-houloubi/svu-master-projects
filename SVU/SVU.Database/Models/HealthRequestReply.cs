using SVU.Database.Models.Base;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SVU.Database.Models
{
    /// <summary>
    /// The replies of any health requests
    /// </summary>
    [Table("RequestReplies", Schema = "Health")]
    public class HealthRequestReply : BaseEntityModel
    {
        #region Properties
        [Required]
        [StringLength(400)]
        public string Content { get; set; }

        [ForeignKey(nameof(User))]
        public Guid UserId { get; set; }

        [ForeignKey(nameof(Request))]
        public Guid RequestId { get; set; }
        #endregion

        #region Navigation Properties

        public HealthUser User { get; set; }

        public HealthRequest Request { get; set; }
        #endregion
        #region Constructer
        /// <summary>
        /// Default constructer
        /// </summary>
        public HealthRequestReply()
        {

        }
        #endregion
    }
}
