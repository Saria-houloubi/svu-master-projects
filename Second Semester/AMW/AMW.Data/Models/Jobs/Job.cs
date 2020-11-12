using AMW.Data.Attributes;
using AMW.Data.Models.Base;
using AMW.Shared.Extensions;
using AMW.Shared.Models;
using Newtonsoft.Json;

namespace AMW.Data.Models.Jobs
{
    public class Job : BaseEntity
    {
        #region Properties
        [SqlParam]
        public string Title { get; set; }
        [SqlParam]
        [JsonIgnore]
        public int CompanyId { get; set; }
        [SqlParam]
        public string EducationLevel { get; set; }
        [SqlParam]
        public int ExperienceYears { get; set; }
        [SqlParam]
        public int Salery { get; set; }
        #endregion


        public override void ParseData(AmwSqlDataReaderWrapper reader)
        {
            base.ParseData(reader);

            if (reader.Reader.HasRows)
            {
                Title = reader.GetValueIfExisits<string>(nameof(Title));
                EducationLevel = reader.GetValueIfExisits<string>(nameof(EducationLevel));
                ExperienceYears = reader.GetValueIfExisits<int>(nameof(ExperienceYears));
                Salery = reader.GetValueIfExisits<int>(nameof(Salery));
            }

        }
    }
}
