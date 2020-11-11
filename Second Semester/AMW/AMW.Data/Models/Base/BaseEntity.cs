using AMW.Data.Abstraction.Parsing;
using AMW.Data.Attributes;
using AMW.Data.Attributes.Swagger;
using AMW.Shared.Extensions;
using AMW.Shared.Models;
using System;
using System.Collections.Generic;

namespace AMW.Data.Models.Base
{
    public abstract class BaseEntity: IDataParse<AmwSqlDataReaderWrapper>
    {
        #region Properties
        [SqlParam]
        [SwaggerIgnore]
        public int Id { get; set; }
        [SwaggerIgnore]
        public DateTime CreationDate { get; set; }
        [SwaggerIgnore]
        public DateTime LastUpdatedDate { get; set; }

        /// <summary>
        /// Holds any runtime extra properties that could be assigns to the entit
        /// </summary>
        [SwaggerIgnore]
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
        #endregion


        public virtual void ParseData(AmwSqlDataReaderWrapper reader)
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
    }
}
