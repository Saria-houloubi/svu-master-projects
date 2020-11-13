using AMW.Data.Models.Base;
using AMW.Shared.Extensions;
using AMW.Shared.Models;

namespace AMW.Data.Models.Educations
{
    public class EducationLevel : BaseEntity
    {
        #region Properties

        public string Level { get; set; }
        #endregion


        public override void ParseData(AmwSqlDataReaderWrapper reader)
        {
            base.ParseData(reader);

            if (reader.HasRows)
            {
                Level = reader.GetValueIfExisits<string>(nameof(Level));
            }
        }
    }
}
