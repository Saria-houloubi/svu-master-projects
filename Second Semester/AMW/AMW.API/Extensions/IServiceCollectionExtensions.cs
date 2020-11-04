using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AMW.API.Extensions
{
    public static class IServiceCollectionExtensions
    {

        public static void AddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen();
        }
    }
}
