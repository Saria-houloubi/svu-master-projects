using AMW.Data.Attributes;
using AMW.Data.Models.Base;

namespace AMW.Data.Models.Candidates
{
    public class CandidateFilter : BaseEntityFilter
    {
        #region Properties
        [SqlParam]
        public string Login { get; set; }
        [SqlParam]
        public string EducationLevel { get; set; }
        #endregion
    }
}
