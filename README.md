
# SwaggerWrapper

SwaggerWrapper simplifies the integration of OpenAPI and Swagger documentation generation in your ASP.NET Core applications. It provides an easy way to configure and manage your API documentation with minimal setup.

## Feature

- Simplifies the setup of Swagger and OpenAPI documentation.
- Provides default settings that can be customized via `appsettings.json`.
- Easy integration with your ASP.NET Core 8 application.

## Installation

1. Install the SwaggerWrapper package via NuGet:
   ```sh
   dotnet add package SwaggerWrapper
   ```

2. Add the following settings to your `appsettings.json` file if you want to customize the default values:

   ```json
   {
       "OpenApiSetting": {
           "Version": "v1",
           "Title": "Example",
           "Description": "Example Api",
           "TermsOfService": "https://example.com/terms",
           "Contact": {
               "Name": "Jon Doe",
               "Url": "https://example.com/jon"
           },
           "JwtSecurity": {
               "Name": "Authorize",
               "Description": "Input your Access token to access this API",
               "BearerFormat": "JWT"
           },
           "Options": {
               "EnableJwtAuthButton": false,
               "EnableCustomSchemaFilter": true
           },
           "LicenseInfo": {
               "Name": "MIT",
               "Url": "https://mit-license.org/"
           }
       }
   }
   ```

## Usage

1. **Register the SwaggerWrapper service in `Startup.cs` or `Program.cs`:**

   ```csharp
   public void ConfigureServices(IServiceCollection services)
   {
       services.AddSwaggerWrapper(Configuration);
   }
   ```

2. **Add SwaggerWrapper to the middleware pipeline in `Startup.cs` or `Program.cs`:**

   ```csharp
   public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
   {
       app.UseSwaggerWrapper();
       // Other middlewares
   }
   ```

## Configuration

The following settings are available in the `appsettings.json` file under the `OpenApiSetting` section:

- `Version`: The version of the API (default: "v1").
- `Title`: The title of the API (default: "Example").
- `Description`: The description of the API (default: "Example Api").
- `TermsOfService`: The URL of the terms of service (default: "https://example.com/terms").
- `Contact`: Contact information for the API.
  - `Name`: The name of the contact person (default: "Jon Doe").
  - `Url`: The URL of the contact person (default: "https://example.com/jon").
- `JwtSecurity`: JWT security definition.
  - `Name`: The name of the security scheme (default: "Authorize").
  - `Description`: Description of the security scheme (default: "Input your Access token to access this API").
  - `BearerFormat`: Bearer format for the security scheme (default: "JWT").
- `Options`: Additional configuration options.
  - `EnableAuthFilter`: Enables the authorization filter (default: false).
  - `EnableCustomSchemaFilter`: Enables custom schema filter (default: true).
- `LicenseInfo`: License information for the API.
  - `Name`: The name of the license (default: "MIT").
  - `Url`: The URL of the license (default: "https://mit-license.org/").

## Contributing

Contributions are welcome! Please feel free to submit a Pull Request.

## License

This project is licensed under the MIT License. See the `LICENSE` file for more information.
