using SVU.Database.Models.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        public string Subject { get; set; }
        public string Content { get; set; }
        [ForeignKey(nameof(User))]
        public Guid HealthUserId { get; set; }
        /// <summary>
        /// For fast order 
        /// the count of replies this request has
        /// </summary>
        public int ReplyCount { get; set; }
        #endregion


        #region Navigation Properties

        public HealthUser User { get; set; }

        public IEnumerable<HealthRequestReply> Replies { get; set; }
        #endregion

        #region Constructer
        /// <summary>
        /// Default constructer
        /// </summary>
        public HealthRequest()
        {
            Replies = new Collection<HealthRequestReply>();

        }
        #endregion
    }
}
