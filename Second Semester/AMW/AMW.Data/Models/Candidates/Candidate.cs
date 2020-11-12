using AMW.Data.Abstraction.Parsing;
using AMW.Data.Attributes;
using AMW.Data.Models.Base;
using AMW.Shared.Extensions;
using AMW.Shared.Models;
using System.ComponentModel.DataAnnotations;

namespace AMW.Data.Models.Candidates
{
    public class Candidate : BaseEntity
    {
        #region Properties
        [Required]
        [SqlParam]
        public string FullName { get; set; }
        [SqlParam]
        public string Tel { get; set; }
        [SqlParam]
        public int Experince { get; set; }
        [SqlParam]
        public string EducationLevel { get; set; }
        #endregion

        #region Constructer
        /// <summary>
        /// Default constructer
        /// </summary>
        public Candidate()
        {

        }
        #endregion

        public override void ParseData(AmwSqlDataReaderWrapper reader)
        {
            base.ParseData(reader);

            FullName = reader.GetValueIfExisits<string>(nameof(FullName));
            Tel = reader.GetValueIfExisits<string>(nameof(Tel));
            Experince = reader.GetValueIfExisits<int>(nameof(Experince));
            EducationLevel = reader.GetValueIfExisits<string>(nameof(EducationLevel));
        }
    }
}
