using Asp.Versioning;
using Commons.Components;
using Microsoft.Extensions.DependencyInjection;

namespace Commons.Extensions;

public static class ServiceExtensions
{
    public static IServiceCollection RegisterSwaggerServices(this IServiceCollection services, string serviceName)
    {
        var apiVersioningBuilder = services.AddApiVersioning(options =>
        {
            options.DefaultApiVersion = new ApiVersion(1, 0);
            options.AssumeDefaultVersionWhenUnspecified = true;
            options.ReportApiVersions = true;
            options.ApiVersionReader = ApiVersionReader.Combine(
                new UrlSegmentApiVersionReader(),
                new QueryStringApiVersionReader("x-api-version"),
                new HeaderApiVersionReader("x-api-version")
            );
        });
        
        apiVersioningBuilder.AddApiExplorer(options =>
        {
            options.GroupNameFormat = "'v'VVV";
            options.SubstituteApiVersionInUrl = true;
        });

        services.AddSingleton(new ApiConfig() { Name = serviceName });
        services.ConfigureOptions<SwaggerConfigureOptions>();
        
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        return services;
    }
}