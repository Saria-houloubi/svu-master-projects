using AMW.API.Framework.Abstracion;
using AMW.Core.IServices;
using AMW.Core.Services;
using AMW.Core.Services.Candidates;
using AMW.Data.Models.Candidates;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

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
            services.AddSingleton<IRepositoryService<Candidate>, CandidateService>();
        }
    }
}
