using AMW.API.Models.Swagger;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;

namespace AMW.API.Extensions
{
    public static class IApplicationBuilderExtensions
    {
        public static void UseAwmSwagger(this IApplicationBuilder buider, IConfiguration configuration)
        {
            var config = new AwmSwaggerConfig();

            configuration.GetSection("swagger").Bind(config);

            buider.UseSwagger();

            buider.UseSwaggerUI(options =>
            {
                options.RoutePrefix = config.RoutePrfix;
                options.SwaggerEndpoint(config.UIREndPoint, config.Title);
            });
        }
    }
}
