using AMW.API.Framework.Abstracion;
using AMW.API.Framework.Models.Swagger;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
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
            services.AddSwaggerGen(options =>
            {
                options.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Scheme = JwtBearerDefaults.AuthenticationScheme,
                    BearerFormat = "Bearer {token}",
                    Description = "Some APIs require authentication using bearer token that could be retrived from login \r\n" +
                    "The header should as Bearer [token]",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme()
                        {
                            Reference = new OpenApiReference()
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = JwtBearerDefaults.AuthenticationScheme
                            }
                        },
                        Array.Empty<string>()
                    }

                });

            });

            services.AddSwaggerGenNewtonsoftSupport();
        }
    }
}
