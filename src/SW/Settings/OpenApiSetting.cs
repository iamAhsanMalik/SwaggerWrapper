namespace SW.Settings;

public sealed class OpenApiSetting // Make it always public as it will be bound to appsetting
{
    public const string SectionName = nameof(OpenApiSetting);
    public string? Version { get; set; } = "v1";
    public string? Title { get; set; } = "Example";
    public string? Description { get; set; } = "Example Api";
    public string? TermsOfService { get; set; } = "https://example.com/terms";
    public required ContactInfo Contact { get; set; }
    public JwtSecurityDefinition? JwtSecurity { get; set; }
    public ConfigureOptions? Options { get; set; }
    public License? LicenseInfo { get; set; }
    public class License
    {
        public string? Name { get; set; } = "MIT";
        public string? Url { get; set; } = "https://mit-license.org/";
    }
    public class ContactInfo
    {
        public string? Name { get; set; } = "Jon Doe";
        public string? Url { get; set; } = "https://example.com/jon";
    }
    public class JwtSecurityDefinition
    {
        public string? Name { get; set; } = "Authorize";
        public string? Description { get; set; } = "Input your Access token to access this API";
        public string? BearerFormat { get; set; } = "JWT";
    }
    public class ConfigureOptions
    {
        public bool EnableJwtAuthButton { get; set; } = false;
        public bool EnableCustomSchemaFilter { get; set; } = true;
    }
}
