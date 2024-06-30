using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;

using SW.Extensions;
using SW.Filters;
using SW.Settings;

using Swashbuckle.AspNetCore.SwaggerGen;

namespace SW;

public static class ConfigureOpenApi
{
    #region Open Api Dependency Extension
    public static IServiceCollection AddSwaggerWrapper(this IServiceCollection services, IConfiguration configuration)
    {
        return services
                    .Configure<OpenApiSetting>(configuration.GetSection(OpenApiSetting.SectionName))
                    .OpenApiConfiguration();
    }
    #endregion

    #region Open Api Service
    internal static IServiceCollection OpenApiConfiguration(this IServiceCollection services)
    {
        // Get Open Api setting
        var openApiSettings = services.BuildServiceProvider().GetRequiredService<IOptions<OpenApiSetting>>().Value;

        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(options =>
        {
            options.CustomSchemaIds(type => type.FriendlyId().Replace("[", "<").Replace("]", ">"));

            if (openApiSettings.Options?.EnableCustomSchemaFilter is true)
            {
                options.SchemaFilter<CustomSchemaNameFilter>();
            }

            if (openApiSettings.Options?.EnableJwtAuthButton is true)
            {
                options.OperationFilter<AddJwtAuthParameter>();
            }
            options.AddSwaggerDocumentation(openApiSettings);
        });
        return services;
    }

    private static OpenApiSecurityScheme SecurityDefinitions(OpenApiSetting openApiSettings)
    {
        return new OpenApiSecurityScheme
        {
            Name = openApiSettings.JwtSecurity?.Name,
            Description = openApiSettings.JwtSecurity?.Description,
            In = ParameterLocation.Header,
            Type = SecuritySchemeType.Http,
            BearerFormat = openApiSettings.JwtSecurity?.BearerFormat,
        };
    }
    private static void AddSwaggerDocumentation(this SwaggerGenOptions options, OpenApiSetting openApiSettings)
    {

        options.SwaggerDoc(openApiSettings.Version, CreateOpenApiInfo(openApiSettings));
    }
    private static OpenApiInfo CreateOpenApiInfo(OpenApiSetting openApiSettings)
    {
        var openApiInfo = new OpenApiInfo
        {
            Version = Convert.ToString(1.0),
            Title = openApiSettings.Title,
            Description = openApiSettings.Description,
            TermsOfService = new Uri(openApiSettings.TermsOfService ?? default!)
        };

        openApiInfo.Contact = new OpenApiContact
        {
            Name = openApiSettings.Contact?.Name,
            Url = new Uri(openApiSettings.Contact?.Url ?? default!)
        };
        openApiInfo.License = new OpenApiLicense
        {
            Name = openApiSettings.LicenseInfo?.Name,
            Url = new Uri(openApiSettings.LicenseInfo?.Url ?? default!)
        };
        return openApiInfo;
    }
    #endregion

    #region Open Api Middleware
    public static IApplicationBuilder UseSwaggerWrapper(this IApplicationBuilder app)
    {
        app.UseSwagger();
        app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "V1"));

        return app;
    }
    #endregion
}
