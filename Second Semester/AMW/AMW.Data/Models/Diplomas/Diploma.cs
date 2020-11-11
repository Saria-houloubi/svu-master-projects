using AMW.Data.Attributes;
using AMW.Data.Attributes.Swagger;
using AMW.Data.Models.Base;
using AMW.Shared.Extensions;
using AMW.Shared.Models;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace AMW.Data.Models.Diplomas
{
    public class Diploma : BaseEntity
    {
        #region Properties
        [SqlParam]
        [Required]
        public string Title { get; set; }

        [SqlParam]
        [SwaggerIgnore]
        [JsonIgnore]
        public int CandidateId { get; set; }
        #endregion

        public override void ParseData(AmwSqlDataReaderWrapper reader)
        {
            base.ParseData(reader);

            Title = reader.GetValueIfExisits<string>(nameof(Title));
        }
    }
}
