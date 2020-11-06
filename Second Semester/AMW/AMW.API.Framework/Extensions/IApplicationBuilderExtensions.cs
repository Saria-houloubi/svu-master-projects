using AMW.API.Framework.Abstracion;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using System.Linq;
using System.Reflection;

namespace AMW.API.Framework.Extensions
{
    public static partial class IApplicationBuilderExtensions
    {
        public static void Configure(this IApplicationBuilder app, IHostingEnvironment env, IConfiguration configuration)
        {
            var assembly = Assembly.GetAssembly(typeof(IAmwStartUp));

            var startups = assembly.GetInstancesOfType<IAmwStartUp>()?.OrderBy(item => item.Order);

            foreach (var item in startups)
            {
                item.Configure(app, env, configuration);
            }
        }
    }
}
