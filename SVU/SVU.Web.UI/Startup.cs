using BotDetect.Web;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
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
using SVU.Web.UI.Middlewares;
using SVU.Web.UI.Models.Configuration;
using System;

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
            //The environment that we are working in
            var envName = services.BuildServiceProvider().GetService<IHostingEnvironment>().EnvironmentName;

            //Add the wanted services to the DI pipeline
            switch (envName.ToLower())
            {
                case "development":
                    services.AddSingleton<ILoggingService, DefaultLoggingSservice>();
                    break;
                case "production":
                    services.AddSingleton<ILoggingService, SaveMyDataLoggingService>();
                    break;
                default:
                    break;
            }

            services.Configure<LoggingRestrictionOptions>(Configuration.GetSection(LoggingRestrictionOptions.SectionName));

            services.AddSingleton<IMemoryCache, MemoryCache>();
            //Used for botdetector nuget pacages 
            //  for more information see https://captcha.com/mvc/mvc-captcha.html#configure-app-netcore2.1-or-higher
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddMemoryCache();

            services.AddScoped<IInitializeDatabaseService, MSSQLInitializeDatabaseService>();
            services.AddScoped<ICourseDatabaseService, MSSQLCourseDatabaseService>();
            services.AddScoped<IFileDatabaseService, MSSQLFileDatabaseService>();
            services.AddScoped<IDataSetDatabaseService, MSSQLDataSetDatabaseService>();
            services.AddScoped<IHealthAccountService, MSSQLHealthAccountService>();
            services.AddScoped<IHealthBlogService, MSSQLHealthBlogService>();

            //Add the database context to DI piplline
            services.AddDbContext<SVUDbContext>(options =>
            {
                //Connecte to the right connection
                options.UseSqlServer(Configuration.GetConnectionString(envName));
            });

            //Add cookies to the request pipeline
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.LoginPath = "/homework/Awp";
                });

            services.AddApplicationInsightsTelemetry();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);


            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30);
                options.Cookie.IsEssential = true;
            });
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

            app.UseStaticFiles();
            //Order is important
            app.UseSession();
            //Add the third party nuget captcha
            app.UseCaptcha(Configuration);

            app.UseMiddleware<CustomLoggingMiddleware>();

            app.UseAuthentication();


            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
