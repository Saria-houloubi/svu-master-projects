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
        #endregion

        #region Constructer
        /// <summary>
        /// Default constructer
        /// </summary>
        public Candidate()
        {

        }

        /// <summary>
        /// Sqldata reader
        /// </summary>
        public Candidate(AwmSqlDataReaderWrapper reader)
            : base(reader)
        {
            FullName = reader.GetValueIfExisits<string>(nameof(FullName));
            Tel = reader.GetValueIfExisits<string>(nameof(Tel));
            Experince = reader.GetValueIfExisits<int>(nameof(Experince));
        }
        #endregion
    }
}
