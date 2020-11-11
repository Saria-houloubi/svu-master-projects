using AMW.Data.Attributes;
using AMW.Data.Attributes.Swagger;
using AMW.Data.Models.Base;

namespace AMW.Data.Models.Companies
{
    public class CompanyFilter : BaseEntityFilter
    {
        #region Properties
        [SqlParam]
        [SwaggerIgnore]
        public string Login { get; set; }
        [SqlParam]
        public string Name { get; set; }
        [SqlParam]
        public string Tel { get; set; }
        #endregion
    }
}
