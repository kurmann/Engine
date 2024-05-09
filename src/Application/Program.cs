using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace Kurmann.Videoschnitt.Engine.Application;

internal class Program
{
    static void Main(string[] args) => CreateHostBuilder(args).Build().Run();

    public static IHostBuilder CreateHostBuilder(string[] args)
    {
        return Host.CreateDefaultBuilder(args)
            .ConfigureAppConfiguration((hostingContext, config) =>
            {
                if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == Environments.Development)
                {
                    // execute "dotnet user-secrets init" in the project folder to create the secrets.json file
                    // add specific secrets with "dotnet user-secrets set "Kurmann:Videoschnitt:MikaModule:SampleSetting" "Secret Value""
                    config.AddUserSecrets<Program>();
                }
            })
            .ConfigureServices((hostContext, services) =>
            {
                services.AddEngine(hostContext.Configuration);
            });
    }
}
