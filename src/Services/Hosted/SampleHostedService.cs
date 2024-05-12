using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Kurmann.Videoschnitt.Engine.Hosted;

public class SampleHostedService(ILogger<SampleHostedService> logger) : IHostedService, IDisposable
{
    private readonly ILogger<SampleHostedService> _logger = logger;
    private Timer? _timer;

    public Task StartAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Sample Service is starting.");

        _timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromSeconds(5));

        return Task.CompletedTask;
    }

    private void DoWork(object? state)
    {
        _logger.LogInformation("Sample Service is working. Current time: {time}", DateTimeOffset.Now);
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Sample Service is stopping.");

        _timer?.Change(Timeout.Infinite, 0);

        return Task.CompletedTask;
    }

    public void Dispose()
    {
        _timer?.Dispose();
        GC.SuppressFinalize(this);
    }
}