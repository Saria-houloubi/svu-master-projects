using AMW.Data.Attributes;
using AMW.Data.Attributes.Swagger;
using AMW.Data.Models.Base;
using Newtonsoft.Json;

namespace AMW.Data.Models.Diplomas
{
    public class DiplomaFilter : BaseEntityFilter
    {
        #region Properties
        [SqlParam]
        public string Title { get; set; }


        [SqlParam]
        [SwaggerIgnore]
        [JsonIgnore]
        public int CandidateId { get; set; }
        #endregion
    }
}
