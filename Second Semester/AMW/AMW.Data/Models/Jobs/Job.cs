using AMW.Data.Attributes;
using AMW.Data.Attributes.Swagger;
using AMW.Data.Models.Base;
using AMW.Shared.Extensions;
using AMW.Shared.Models;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace AMW.Data.Models.Jobs
{
    public class Job : BaseEntity
    {
        #region Properties
        [SqlParam]
        [Required]
        public string Title { get; set; }
        [SqlParam]
        [SwaggerIgnore]
        public int CompanyId { get; set; }
        [SwaggerIgnore]
        public string CompanyName { get; set; }
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
                CompanyId = reader.GetValueIfExisits<int>(nameof(CompanyId));
                CompanyName = reader.GetValueIfExisits<string>(nameof(CompanyName));
            }

        }
    }
}
