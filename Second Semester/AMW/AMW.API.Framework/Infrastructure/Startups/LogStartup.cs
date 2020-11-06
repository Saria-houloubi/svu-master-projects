using AMW.API.Framework.Abstracion;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AMW.API.Framework.Infrastructure.Startups
{
    public class LogStartup : IAmwStartUp
    {
        public int Order => 2;

        public void Configure(IApplicationBuilder builder, IHostingEnvironment env, IConfiguration configuration)
        {
        }

        public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            log4net.Config.XmlConfigurator.ConfigureAndWatch(new System.IO.FileInfo(configuration["log4net:configFile"]));
        }
    }
}
