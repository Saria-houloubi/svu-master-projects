using AMW.Data.Abstraction.Parsing;
using AMW.Data.Attributes;
using AMW.Data.Models.Base;
using AMW.Shared.Extensions;
using AMW.Shared.Models;
using System.ComponentModel.DataAnnotations;

namespace AMW.Data.Models.Companies
{
    public class Company : BaseEntity
    {
        #region Properties
        [SqlParam]
        [Required]
        public string Name { get; set; }
        [SqlParam]
        public string Tel { get; set; }
        #endregion


        public override void ParseData(AmwSqlDataReaderWrapper reader)
        {
            base.ParseData(reader);

            Name = reader.GetValueIfExisits<string>(nameof(Name));
            Tel = reader.GetValueIfExisits<string>(nameof(Tel));
        }
    }
}
