using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Kurmann.Videoschnitt.Engine.Hosted;

public class BasicFileWatcherService(ILogger<BasicFileWatcherService> logger, IOptionsSnapshot<EngineSettings> options) : IHostedService, IDisposable
{
    private readonly ILogger<BasicFileWatcherService> _logger = logger;
    private readonly EngineSettings _servicesSettings = options.Value;
    private readonly List<FileSystemWatcher> _watchers = [];

    public Task StartAsync(CancellationToken cancellationToken)
    {
        var watchDirectories = _servicesSettings.WatchDirectories;

        if (watchDirectories == null || watchDirectories.Length == 0)
        {
            _logger.LogWarning("No directories to watch are configured.");
            return Task.CompletedTask;
        }

        _logger.LogInformation("Watching directories: {directories}", string.Join(", ", watchDirectories));

        foreach (var directory in watchDirectories)
        {
            var watcher = new FileSystemWatcher
            {
                Path = directory,
                IncludeSubdirectories = true,
                NotifyFilter = NotifyFilters.LastWrite | NotifyFilters.FileName,
            };

            watcher.Created += OnCreated;
            watcher.Renamed += OnRenamed;
            watcher.Deleted += OnDeleted;
            watcher.Changed += OnChanged;
            watcher.EnableRaisingEvents = true;

            // Speichern Sie die Instanz, um sie später freizugeben
            _watchers.Add(watcher);
        }

        _logger.LogInformation("Media File Watcher Service is starting.");

        return Task.CompletedTask;
    }

    private void OnCreated(object sender, FileSystemEventArgs e)
    {
        _logger.LogInformation("File created: {file}", e.FullPath);
    }

    private void OnRenamed(object sender, RenamedEventArgs e)
    {
        _logger.LogInformation("File renamed: {file}", e.FullPath);
    }

    private void OnDeleted(object sender, FileSystemEventArgs e)
    {
        _logger.LogInformation("File deleted: {file}", e.FullPath);
    }

    private void OnChanged(object sender, FileSystemEventArgs e)
    {
        _logger.LogInformation("File changed: {file}", e.FullPath);
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Media File Watcher Service is stopping.");

        foreach (var watcher in _watchers)
        {
            watcher.EnableRaisingEvents = false;
            watcher.Dispose();
        }

        return Task.CompletedTask;
    }

    public void Dispose()
    {
        foreach (var watcher in _watchers)
        {
            watcher.Dispose();
        }
        GC.SuppressFinalize(this);
    }
}