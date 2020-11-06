using AMW.Data.Attributes;
using AMW.Shared.Extensions;
using AMW.Shared.Models;
using System;

namespace AMW.Data.Models.Base
{
    public abstract class BaseEntity
    {
        #region Properties
        [SqlParam]
        public int Id { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime LastUpdatedDate { get; set; }
        #endregion

        #region Constructers
        /// <summary>
        /// Default constructer
        /// </summary>
        public BaseEntity()
        {

        }

        /// <summary>
        /// Sql reader constructer
        /// </summary>
        /// <param name="reader"></param>
        public BaseEntity(AwmSqlDataReaderWrapper reader)
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
