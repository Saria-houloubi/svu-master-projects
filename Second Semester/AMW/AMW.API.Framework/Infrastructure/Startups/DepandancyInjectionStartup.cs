using AMW.API.Framework.Abstracion;
using AMW.Core.IServices;
using AMW.Core.Services;
using AMW.Core.Services.Candidates;
using AMW.Core.Services.Companies;
using AMW.Core.Services.Diplomas;
using AMW.Core.Services.Educations;
using AMW.Data.Models.Amw;
using AMW.Data.Models.Candidates;
using AMW.Data.Models.Companies;
using AMW.Data.Models.Diplomas;
using AMW.Data.Models.Educations;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AMW.API.Framework.Infrastructure.Startups
{
    public class DepandancyInjectionStartup : IAmwStartUp
    {
        public int Order => 2;

        public void Configure(IApplicationBuilder builder, IHostingEnvironment env, IConfiguration configuration)
        {
        }

        public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IDatabaseExecuterService, SqlServerDatabaseExecuterService>();

            services.AddSingleton<ILookUpRepository<EducationLevel>, EducationLevelLookupService>();


            services.AddSingleton<IRepositoryService<Candidate>, CandidateService>();
            services.AddSingleton<IRepositoryService<Company>, CompanyService>();
            services.AddSingleton<IRepositoryService<Diploma>, DiplomaService>();


            services.AddSingleton<IAuthService<Candidate, AmwSecure>, CandidateAuthService>();
            services.AddSingleton<IAuthService<Company, AmwSecure>, CompanyAuthService>();
        }
    }
}
