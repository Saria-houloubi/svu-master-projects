using AMW.Data.Attributes;
using AMW.Data.Models.Base;
using System;

namespace AMW.Data.Models.Jobs
{
    public class JobFilter : BaseEntityFilter
    {
        #region Proeprties
        [SqlParam]
        public string Title { get; set; }
        [SqlParam]
        public Nullable<int> Company { get; set; }
        [SqlParam]
        public string EducationLevel { get; set; }
        [SqlParam]
        public Nullable<int> ExperienceYears { get; set; }
        [SqlParam]
        public Nullable<int> Salery { get; set; }
        #endregion
    }
}
