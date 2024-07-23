using Asp.Versioning.ApiExplorer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Commons.Extensions;

public static class WebApplicationExtensions
{
    public static WebApplication ConfigureSwaggerPipeline(this WebApplication webApp, string serviceName)
    {
        // Configure the HTTP request pipeline.
        if (webApp.Environment.IsDevelopment())
        {
            webApp.UseSwagger();
            webApp.UseSwaggerUI(options =>
            {
                var versionDescriptor = webApp.Services.GetRequiredService<IApiVersionDescriptionProvider>();
                foreach (var descriptor in versionDescriptor.ApiVersionDescriptions)
                {
                    options.SwaggerEndpoint($"/swagger/{descriptor.GroupName}/swagger.json",
                        $"{serviceName} {descriptor.GroupName}");
                }
            });
        }

        return webApp;
    }
}