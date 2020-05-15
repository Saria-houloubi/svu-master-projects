using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SVU.Database.DatabaseContext;
using SVU.Database.IService;
using SVU.Database.Service.MSSQL;
using SVU.Logging.IServices;
using SVU.Logging.Services;

namespace SVU.Web.UI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //Add the wanted services to the DI pipeline
            services.AddSingleton<ILoggingService, DefaultLoggingSservice>();
            services.AddSingleton<IMemoryCache, MemoryCache>();


            services.AddScoped<IInitializeDatabaseService, MSSQLInitializeDatabaseService>();
            services.AddScoped<ICourseDatabaseService, MSSQLCourseDatabaseService>();
            services.AddScoped<IFileDatabaseService, MSSQLFileDatabaseService>();
            services.AddScoped<IDataSetDatabaseService, MSSQLDataSetDatabaseService>();

            //Add the database context to DI piplline
            services.AddDbContext<SVUDbContext>(options =>
            {
                //Get the env that the project is working on
                var envName = services.BuildServiceProvider().GetService<IHostingEnvironment>().EnvironmentName;
                //Connecte to the right connection
                options.UseSqlServer(Configuration.GetConnectionString(envName));
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IInitializeDatabaseService initializeDatabaseService)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            //Create the database if not found
            initializeDatabaseService.InitailizeDatabase();

            //app.UseHttpsRedirection();

            app.UseStaticFiles();


            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
