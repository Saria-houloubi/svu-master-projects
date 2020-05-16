using SVU.Database.Models.Base;
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

        public string Content { get; set; }
        /// <summary>
        /// A flag if true then the reply was from the requester side
        /// </summary>
        public bool IsRequesterSide { get; set; }
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
