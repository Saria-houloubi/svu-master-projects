using AMW.API.Framework.Abstracion;
using AMW.API.Framework.Models.Swagger;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace AMW.API.Framework.Infrastructure.Startups
{
    public class SwaggerStartup : IAmwStartUp
    {
        /// <summary>
        /// Low priority
        /// </summary>
        public int Order => 3;

        public void Configure(IApplicationBuilder builder, IHostingEnvironment env, IConfiguration configuration)
        {
            var config = new AwmSwaggerConfig();

            configuration.GetSection("swagger").Bind(config);

            builder.UseSwagger();

            builder.UseSwaggerUI(options =>
            {
                options.RoutePrefix = config.RoutePrfix;
                options.SwaggerEndpoint(config.UIREndPoint, config.Title);
            });
        }

        public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddSwaggerGen();
        }
    }
}
