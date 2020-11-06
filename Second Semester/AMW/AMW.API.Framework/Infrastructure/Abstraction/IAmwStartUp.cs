using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AMW.API.Framework.Abstracion
{
    public interface IAmwStartUp
    {
        void ConfigureServices(IServiceCollection services, IConfiguration configuration);

        void Configure(IApplicationBuilder builder, IHostingEnvironment env, IConfiguration configuration);

        /// <summary>
        /// The order on which the startup to be executed at
        /// </summary>
        int Order { get; }
    }
}
