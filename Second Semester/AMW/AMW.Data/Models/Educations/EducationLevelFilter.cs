using AMW.Data.Attributes;
using AMW.Data.Models.Base;

namespace AMW.Data.Models.Educations
{
    public class EducationLevelFilter : BaseEntityFilter
    {
        #region Properties
        [SqlParam]
        public string Level { get; set; }
        #endregion
    }
}
