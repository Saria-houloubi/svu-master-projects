using AMW.API.Framework.Abstracion;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Reflection;

namespace AMW.API.Framework.Extensions
{
    public static partial class IServiceCollectionExtensions
    {
        public static IServiceProvider ConfigureServices(this IServiceCollection services, IConfiguration configuration)
        {
            var assembly = Assembly.GetAssembly(typeof(IAmwStartUp));

            var startups = assembly.GetInstancesOfType<IAmwStartUp>()?.OrderBy(item => item.Order);

            foreach (var item in startups)
            {
                item.ConfigureServices(services, configuration);
            }

            return services.BuildServiceProvider();
        }
    }
}
