using AMW.Data.Attributes.Swagger;
using AMW.Data.Models.Base;
using AMW.Shared.Extensioins.String;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;

namespace AMW.API.Framework.Filter.Swagger
{

    public class SwaggerSchemaFilter : ISchemaFilter
    {
        public void Apply(OpenApiSchema schema, SchemaFilterContext context)
        {
            if (schema == null)
            {
                return;
            }
            //Check if we have recived the full schema information
            if (context.Type.IsSubclassOf(typeof(BaseEntity)))
            {
                //copy the properties to an array for safe edit on schema properties
                var properties = context.Type.GetProperties();

                foreach (var prop in properties)
                {
                    var name = prop.Name.LowerCaseFirstCharacter();
                    if (Attribute.IsDefined(prop, typeof(SwaggerIgnoreAttribute)) && schema.Properties.ContainsKey(name))
                    {
                        schema.Properties.Remove(name);
                    }
                }
            }
        }
    }
}
