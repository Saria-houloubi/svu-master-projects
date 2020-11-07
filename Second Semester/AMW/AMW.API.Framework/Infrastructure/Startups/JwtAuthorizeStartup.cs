using AMW.API.Framework.Abstracion;
using AMW.Data.Models.General;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace AMW.API.Framework.Infrastructure.Startups
{
    public class JwtAuthorizeStartup : IAmwStartUp
    {
        public int Order => 2;

        public void Configure(IApplicationBuilder builder, IHostingEnvironment env, IConfiguration configuration)
        {
        }

        public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            var jwtSettings = new JwtTokenSettings();
            configuration.GetSection("Jwt").Bind(jwtSettings);


            services.AddAuthentication()
                .AddJwtBearer(buider =>
                {
                    buider.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidIssuer = jwtSettings.Issuer,
                        ValidAudience = jwtSettings.Audience,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.SecretKey))
                    };
                });


        }
    }
}
