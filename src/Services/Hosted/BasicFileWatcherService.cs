using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Kurmann.Videoschnitt.Engine.Hosted;

public class BasicFileWatcherService(ILogger<BasicFileWatcherService> logger, IOptions<EngineSettings> options) : IHostedService, IDisposable
{
    private readonly ILogger<BasicFileWatcherService> _logger = logger;
    private readonly EngineSettings _settings = options.Value;
    private FileSystemWatcher? _fileSystemWatcher;

    public Task StartAsync(CancellationToken cancellationToken)
    {
        // Prüfen, ob ein Verzeichnis zum Überwachen konfiguriert ist
        if (string.IsNullOrWhiteSpace(_settings.WatchDirectory))
        {
            _logger.LogWarning("No directory to watch is configured.");
            return Task.CompletedTask;
        }

        // Informationen ausgeben
        _logger.LogInformation("Watching directories: {directories}", string.Join(", ", _settings.WatchDirectory.Split(';')));
        _logger.LogInformation("Media File Watcher Service is starting.");

        // FileSystemWatcher initialisieren
        _fileSystemWatcher = new FileSystemWatcher
        {
            IncludeSubdirectories = true,
            NotifyFilter = NotifyFilters.LastWrite | NotifyFilters.FileName,
            Path = _settings.WatchDirectory
        };

        // Ereignisse abonnieren
        _fileSystemWatcher.Created += OnCreated;
        _fileSystemWatcher.Renamed += OnRenamed;
        _fileSystemWatcher.Deleted += OnDeleted;
        _fileSystemWatcher.Changed += OnChanged;
        _fileSystemWatcher.EnableRaisingEvents = true;

        // Task beenden
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