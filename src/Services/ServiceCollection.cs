using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Kurmann.Videoschnitt.Engine.Hosted;

namespace Kurmann.Videoschnitt.Engine;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddEngine(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        // Bindet Root-Konfigurationswerte an EngineSettings
        services.Configure<EngineSettings>(configuration.GetSection(EngineSettings.SectionName));
        
        // Dienste hinzufügen
        services.AddHostedService<BasicFileWatcherService>();
        
        return services;
    }
}
