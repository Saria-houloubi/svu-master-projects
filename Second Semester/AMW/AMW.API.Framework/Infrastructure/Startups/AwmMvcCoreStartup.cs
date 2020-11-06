using AMW.API.Framework.Abstracion;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AMW.API.Framework.Infrastructure.Startups
{
    public class AmwMvcCoreStartup : IAmwStartUp
    {
        public int Order => 1000;

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IConfiguration configuration)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseMvc();
        }

        public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            var builder = services.AddMvcCore()
                //This is added for swagger to get find api endpoints
                .AddApiExplorer();

            builder.AddFormatterMappings();

            builder.AddDataAnnotations();

            builder.AddJsonFormatters();
        }
    }
}
