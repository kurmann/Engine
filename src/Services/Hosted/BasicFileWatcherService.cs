using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Kurmann.Videoschnitt.Engine.Hosted;

public class BasicFileWatcherService(ILogger<BasicFileWatcherService> logger, IOptionsSnapshot<EngineSettings> options) : IHostedService, IDisposable
{
    private readonly ILogger<BasicFileWatcherService> _logger = logger;
    private readonly EngineSettings _servicesSettings = options.Value;
    private FileSystemWatcher? _fileSystemWatcher;

    public Task StartAsync(CancellationToken cancellationToken)
    {
        var watchDirectories = _servicesSettings.WatchDirectories;

        // Logge Warnung, wenn keine Verzeichnisse zum Überwachen konfiguriert sind
        if (watchDirectories == null || watchDirectories.Length == 0)
        {
            _logger.LogWarning("No directories to watch are configured.");
            return Task.CompletedTask;
        }

        _logger.LogInformation("Watching directories: {directories}", string.Join(", ", watchDirectories));

        _logger.LogInformation("Media File Watcher Service is starting.");

        _fileSystemWatcher = new FileSystemWatcher
        {
            IncludeSubdirectories = true,
            NotifyFilter = NotifyFilters.LastWrite | NotifyFilters.FileName,
        };

        foreach (var directory in watchDirectories)
        {
            _fileSystemWatcher.Path = directory;
            _fileSystemWatcher.Created += OnCreated;
            _fileSystemWatcher.Renamed += OnRenamed;
            _fileSystemWatcher.Deleted += OnDeleted;
            _fileSystemWatcher.Changed += OnChanged;
            _fileSystemWatcher.EnableRaisingEvents = true;
        }

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

        _fileSystemWatcher?.Dispose();

        return Task.CompletedTask;
    }

    public void Dispose()
    {
        _fileSystemWatcher?.Dispose();
        GC.SuppressFinalize(this);
    }
}