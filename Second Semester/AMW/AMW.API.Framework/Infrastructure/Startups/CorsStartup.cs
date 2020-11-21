using AMW.API.Framework.Abstracion;
using AMW.API.Framework.Models.Cors;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AMW.API.Framework.Infrastructure.Startups
{
    public class CorsStartup : IAmwStartUp
    {
        public int Order => 6;

        public void Configure(IApplicationBuilder builder, IHostingEnvironment env, IConfiguration configuration)
        {
            var activePolicyName = configuration["Cors:policy:active"];

            builder.UseCors(activePolicyName);
        }

        public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {

            var corsConfig = new CorsWrapper();

            var activePolicyName = configuration["Cors:policy:active"];

            configuration.GetSection($"Cors:policy:{activePolicyName}").Bind(corsConfig);

            services.AddCors(options =>
            {
                options.AddPolicy(activePolicyName, buider =>
                {
                    buider.WithExposedHeaders(corsConfig.ExposedHeaders);
                    buider.WithOrigins(corsConfig.AllowedOrigins);
                    buider.WithMethods(corsConfig.AllowedMethods);
                    buider.WithHeaders(corsConfig.AllowedHeaders);
                    if (corsConfig.SupportsCredentials)
                    {
                        buider.AllowCredentials();
                    }
                    else
                    {
                        buider.DisallowCredentials();
                    }
                });
            });
        }


    }
}
