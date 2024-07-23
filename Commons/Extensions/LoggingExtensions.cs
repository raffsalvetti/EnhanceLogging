using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Extensions.Hosting;
using Serilog.Extensions.Logging;

namespace Commons.Extensions;
public static class LogginigExtensions {
    public static IServiceCollection RegisterLogingServices(this IServiceCollection serviceCollection) {

        var cfgs = (IConfiguration)serviceCollection.FirstOrDefault(x =>
                typeof(IConfiguration) == x.ServiceType 
                && x.ImplementationInstance != null
            )?.ImplementationInstance;

        if (cfgs?.GetSection("Serilog").Exists() == true)
        {
            var slogBuilder = new LoggerConfiguration();
            slogBuilder.ReadFrom.Configuration(cfgs);
        
            slogBuilder.Enrich.FromLogContext();
            slogBuilder.Enrich.FromGlobalLogContext();

            var sloger = new SerilogLoggerFactory(slogBuilder.CreateLogger());
            serviceCollection.AddSingleton<ILoggerFactory>((Func<IServiceProvider, ILoggerFactory>) (services => (ILoggerFactory) sloger));
            var implementationInstance = new DiagnosticContext(null);
            serviceCollection.AddSingleton<DiagnosticContext>(implementationInstance);
            serviceCollection.AddSingleton<IDiagnosticContext>((IDiagnosticContext) implementationInstance);
        }
        else
        {
            serviceCollection.AddSingleton(LoggerFactory.Create(b =>
            {
                b.AddConsole(options =>
                {
                    options.FormatterName = "Simple";
                    options.TimestampFormat = "yyyy-MM-dd HH:mm:ss ";
                    options.IncludeScopes = false;
                    options.DisableColors = false;
                });
            }));
        }

        return serviceCollection;
    }
}