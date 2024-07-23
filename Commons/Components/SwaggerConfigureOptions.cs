using Asp.Versioning.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Commons.Components;

public class SwaggerConfigureOptions : IConfigureOptions<SwaggerGenOptions>
{
    private readonly IApiVersionDescriptionProvider _provider;
    private readonly ApiConfig _apiConfig;

    public SwaggerConfigureOptions(IApiVersionDescriptionProvider provider, ApiConfig apiConfig)
    {
        _provider = provider;
        _apiConfig = apiConfig;
    }

    public void Configure(SwaggerGenOptions options)
    {
        foreach (var description in _provider.ApiVersionDescriptions)
        {
            options.SwaggerDoc(description.GroupName, 
                description.IsDeprecated
                    ? new OpenApiInfo()
                    {
                        Title = _apiConfig.Name,
                        Version = description.ApiVersion.ToString(),
                        Description = " This version was deprecated."
                    }
                    :
                    new OpenApiInfo()
                    {
                        Title = _apiConfig.Name,
                        Version = description.ApiVersion.ToString()
                    });
        }
    }

    public void Configure(string name, SwaggerGenOptions options) => Configure(options);
}