using AMW.Data.Attributes;
using AMW.Shared.Extensions;
using AMW.Shared.Models;
using System;
using System.Collections.Generic;

namespace AMW.Data.Models.Base
{
    public abstract class BaseEntity
    {
        #region Properties
        [SqlParam]
        public int Id { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime LastUpdatedDate { get; set; }

        /// <summary>
        /// Holds any runtime extra properties that could be assigns to the entit
        /// </summary>
        public Dictionary<string, object> Extra { get; set; }
        #endregion

        #region Constructers
        /// <summary>
        /// Default constructer
        /// </summary>
        public BaseEntity()
        {
            Extra = new Dictionary<string, object>();
        }

        /// <summary>
        /// Sql reader constructer
        /// </summary>
        /// <param name="reader"></param>
        public BaseEntity(AwmSqlDataReaderWrapper reader)
            : this()
        {
            if (reader == null)
            {
                throw new ArgumentNullException("Sql reader is null");
            }

            if (reader.Reader.HasRows)
            {
                Id = reader.GetValueIfExisits<int>(nameof(Id));
                CreationDate = reader.GetValueIfExisits<DateTime>(nameof(CreationDate));
                LastUpdatedDate = reader.GetValueIfExisits<DateTime>(nameof(LastUpdatedDate));
            }
        }
        #endregion
    }
}
