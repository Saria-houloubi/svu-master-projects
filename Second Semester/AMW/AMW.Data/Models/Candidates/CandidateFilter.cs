using AMW.Data.Attributes;
using AMW.Data.Models.Base;
using Newtonsoft.Json;

namespace AMW.Data.Models.Candidates
{
    public class CandidateFilter : BaseEntityFilter
    {
        #region Properties
        [SqlParam]
        public string Login { get; set; }
        [JsonIgnore]
        public string Password { get; set; }
        #endregion
    }
}
