using AMW.Data.Attributes;
using AMW.Data.Models.Base;

namespace AMW.Data.Models.Jobs
{
    public class JobFilter : BaseEntityFilter
    {
        #region Proeprties
        [SqlParam]
        public string Title { get; set; }
        [SqlParam]
        public int Company { get; set; }
        [SqlParam]
        public string EducationLevel { get; set; }
        [SqlParam]
        public int ExperienceYears { get; set; }
        [SqlParam]
        public int Salery { get; set; }
        #endregion
    }
}
