using Commons.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Commons;

public static class Boot
{
    public static void Run(string[] args, string serviceName, Action<IServiceCollection> customServices = null)
    {
        var builder = WebApplication.CreateBuilder(args);
        
        // Add services to the container.
        builder.Services.AddSingleton<IConfiguration>(builder.Configuration);
        customServices?.Invoke(builder.Services);

        builder.Services.AddControllers();
        builder.Services.RegisterSwaggerServices(serviceName);

        var app = builder.Build();

        app.ConfigureSwaggerPipeline(serviceName);

        app.UseHttpsRedirection();
        app.UseAuthorization();
        app.MapControllers();
        app.Run();
    }
}