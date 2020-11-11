using AMW.Core.IServices;
using AMW.Core.Services.Base;
using AMW.Data.Models.Amw;
using AMW.Data.Models.Companies;
using AMW.Shared.Extensioins.String;
using Microsoft.Extensions.Configuration;
using System.Linq;
using System.Threading.Tasks;

namespace AMW.Core.Services.Companies
{
    public class CompanyAuthService : BaseAuthService<Company, AmwSecure>
    {
        #region Properties
        private readonly IRepositoryService<Company> companySerivce;
        #endregion

        #region Constructer
        /// <summary>
        /// Default constructer
        /// </summary>
        public CompanyAuthService(IConfiguration configuration, IRepositoryService<Company> companySerivce)
            : base(configuration)
        {
            this.companySerivce = companySerivce;
        }
        #endregion

        public override async Task<Company> TryAuthenticateAsync(AmwSecure entity)
        {
            var company = (await companySerivce.GetByFilterAsync(new CompanyFilter()
            {
                Login = entity.Login,
            })).SingleOrDefault();


            if (company != null && company is CompanyRegister fullInfo && fullInfo.Password.VertifyPassword(entity.Password))
            {
                company.Extra.Add("token", CreateJwtToken(company));
                return company;
            }

            return null;
        }
    }
}
