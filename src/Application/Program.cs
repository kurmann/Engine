using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Kurmann.Videoschnitt.Engine;
using Kurmann.Videoschnitt.Engine.Hosted;

internal class Program
{
    static void Main(string[] args) => CreateHostBuilder(args).Build().Run();

    public static IHostBuilder CreateHostBuilder(string[] args)
    {
        return Host.CreateDefaultBuilder(args)
            .ConfigureAppConfiguration((hostingContext, config) =>
            {
                var environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
                if (environmentName == Environments.Development)
                {
                    config.AddUserSecrets<Program>();
                }
                config.AddJsonFile($"src/Application/appsettings.{environmentName}.json", optional: true, reloadOnChange: true);
            })

            .ConfigureServices((hostContext, services) =>
            {
                services.Configure<EngineSettings>(hostContext.Configuration);
                services.AddHostedService<SampleHostedService>();
            });
    }
}