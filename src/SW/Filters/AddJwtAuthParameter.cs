using Microsoft.AspNetCore.Authorization;
using Microsoft.OpenApi.Models;

using Swashbuckle.AspNetCore.SwaggerGen;

namespace SW.Filters;

public sealed class AddJwtAuthParameter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        if (HasAllowAnonymousAttribute(context))
        {
            return;
        }

        operation.Security ??= [];

        var securityScheme = new OpenApiSecurityScheme
        {
            Reference = new OpenApiReference
            {
                Type = ReferenceType.SecurityScheme,
                Id = "Bearer"
            },
        };

        operation.Security.Add(new OpenApiSecurityRequirement
        {
            [securityScheme] = Array.Empty<string>()
        });
    }

    private static bool HasAllowAnonymousAttribute(OperationFilterContext context)
    {
        var actionDescriptor = context.ApiDescription.ActionDescriptor;
        return actionDescriptor?.EndpointMetadata.Any(em => em is AllowAnonymousAttribute) == true;
    }
}
