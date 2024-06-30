using Microsoft.OpenApi.Models;

using SW.Attibutes;

using Swashbuckle.AspNetCore.SwaggerGen;

using System.Reflection;

namespace SW.Filters;

public class CustomSchemaNameFilter : ISchemaFilter
{
    public void Apply(OpenApiSchema schema, SchemaFilterContext context)
    {
        var swaggerNameAttribute = context.Type.GetCustomAttribute<SwaggerSchemaNameAttribute>();
        if (swaggerNameAttribute != null)
        {
            schema.Title = swaggerNameAttribute.Name;
        }
    }
}
