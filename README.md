# Kurmann.Videoschnitt.Engine

## Archiviert

### Dieses Repository wurde in das Monorepo "kurmann/videoschnitt" migriert

**Hinweis:** Dieses Repository wird nicht mehr aktiv weiterentwickelt und ist archiviert. Die Entwicklung wird nun im Monorepo [kurmann/videoschnitt](https://github.com/kurmann/videoschnitt) fortgeführt.

---

## Überblick

Die Kurmann.Videoschnitt.Engine ist das Kernstück unserer Videobearbeitungsplattform und dient als zentrale Steuereinheit, die die verschiedenen spezialisierten Module koordiniert. Diese Module arbeiten zusammen, um umfassende Lösungen im Bereich des Videoschnitts zu bieten. Entwickelt mit dem Ziel, Flexibilität und Robustheit zu maximieren, bietet die Engine eine Plattform für die Integration von Videobearbeitungsmodulen wie Rendering, Schnitt, Effekte und mehr.

## Architektur

### Modularer Aufbau

Die Kurmann.Videoschnitt.Engine ist als modularer Monolith konzipiert, was bedeutet, dass sie aus einzelnen, unabhängigen Modulen besteht, die innerhalb eines einzigen, einheitlichen Prozesses laufen. Dieser Ansatz kombiniert die Einfachheit und Effizienz eines monolithischen Designs mit der Flexibilität und Skalierbarkeit modularer Komponenten. Jedes Modul ist darauf ausgelegt, spezifische Aufgaben innerhalb der Videobearbeitungsplattform zu übernehmen und über eine klar definierte API mit der zentralen Engine zu kommunizieren.

### Engine als Koordinator

Die Hauptrolle der Engine ist es, als Koordinator zu fungieren, der die Ausführung von Aufgaben überwacht, den Datenfluss zwischen den Modulen verwaltet und die Einhaltung der Geschäftslogiken über das gesamte System sicherstellt. Sie ist nicht nur eine Laufzeitumgebung, sondern orchestriert auch die technischen und geschäftlichen Aspekte der Videobearbeitungsprozesse. Durch die zentrale Steuerung der Workflows und die Koordination der Interaktionen zwischen den Modulen sorgt die Engine dafür, dass die Videoprojekte effizient und fehlerfrei von Anfang bis Ende verwaltet werden.

Diese Architektur erleichtert die Wartung und Skalierung der Plattform, indem sie eine klare Trennung der Verantwortlichkeiten ermöglicht und gleichzeitig die Interdependenzen zwischen den Modulen minimiert. So können Entwickler und Techniker schnell auf Veränderungen reagieren und neue Funktionen oder Verbesserungen mit minimalen Störungen für den Gesamtbetrieb implementieren.

## Kernfunktionen

Die Kurmann.Videoschnitt.Engine bietet eine Vielzahl von Kernfunktionen, die darauf ausgelegt sind, eine leistungsfähige und flexible Videobearbeitungsplattform bereitzustellen. Jede dieser Funktionen trägt dazu bei, die Produktivität zu steigern und die Benutzerfreundlichkeit zu verbessern. Hier sind die wichtigsten Funktionen, die von unserer Engine angeboten werden:

### Modulintegration

Die Engine verwendet das `IServiceCollection`-Framework, um die Integration verschiedener Videobearbeitungsmodule flexibel und konfigurierbar zu gestalten. Jedes Modul, wie z.B. Rendering, Schnitt oder Effekte, ist speziell darauf ausgelegt, bestimmte Funktionen zu handhaben und kann nach Bedarf in das Gesamtsystem eingefügt oder entfernt werden. Dies ermöglicht eine anpassbare Lösung, die sich sowohl an kleine als auch an große Produktionsumgebungen anpassen lässt.

### Businesslogik-Orchestrierung

Neben der technischen Koordination der Module steuert die Engine die Geschäftslogik, die die Entscheidungsfindung innerhalb des gesamten Systems leitet. Dies umfasst das Management von Benutzeranforderungen, die Priorisierung von Aufgaben und die Optimierung der Ressourcennutzung. Durch diese zentrale Steuerung wird sichergestellt, dass alle Aktionen im Einklang mit den Geschäftszielen und Kundenanforderungen stehen.

### Workflow-Management

Die Engine ist verantwortlich für die Steuerung der Workflows, die notwendig sind, um Videoprojekte von Anfang bis Ende zu managen. Dies beinhaltet die Planung und Ausführung von Aufgaben, das Management von Abhängigkeiten zwischen den Aufgaben und die Überwachung des Fortschritts. Die Workflow-Management-Funktionen sind darauf ausgerichtet, eine effiziente Durchführung der Videobearbeitungsprozesse zu gewährleisten und die Einhaltung der Projektpläne sicherzustellen.

### Event-Handling und Messaging

Durch die Integration der `Kurmann.Messaging`-Bibliothek unterstützt die Engine ein leistungsstarkes, asynchrones Event-Handling, das eine lose Kopplung zwischen den Komponenten ermöglicht. Die Engine und die Module nutzen diese Funktion, um Zustandsänderungen, wichtige Ereignisse und andere relevante Informationen zu kommunizieren, wodurch eine reaktive und adaptive Systemumgebung geschaffen wird.

## API-Mechanismus der Module

Die Kurmann.Videoschnitt.Engine ist darauf ausgelegt, durch eine gut definierte API die Kommunikation und Interaktion zwischen den einzelnen Modulen und der zentralen Engine zu ermöglichen. Dieses Kapitel beschreibt die verschiedenen Aspekte der API-Struktur, die es Entwicklern ermöglicht, ihre Module so zu implementieren, dass sie nahtlos in die Engine integriert werden können.

### Allgemeine Prinzipien

Die API jedes Moduls ist so gestaltet, dass sie eine klare Trennung zwischen verschiedenen Operationstypen bietet und sich an die Command Query Responsibility Segregation (CQRS) hält. Dieses Prinzip trennt die Befehle (Commands), die den Systemzustand ändern, von den Abfragen (Queries), die Daten abrufen, ohne den Zustand zu ändern. Durch diese Trennung wird die Effizienz gesteigert und die Klarheit der Operationen verbessert.

#### Commands

Commands sind Operationen, die eine Änderung im System bewirken. Sie können synchron oder asynchron ausgeführt werden:

- **Initiate Commands**: Asynchrone Befehle, die einen Prozess starten, und eine Bestätigung über dessen Initiierung zurückgeben. Der Endstatus oder das Ergebnis wird über Events kommuniziert.
  ```csharp
  Task<Result> InitiateCommand(CommandParams parameters);
  ```
- **Direct Commands**: Synchrone Befehle, die sofort ausgeführt werden und direkt eine Antwort auf das Ergebnis der Operation liefern.
  ```csharp
  Result ExecuteCommand(CommandParams parameters);
  ```

#### Queries

Queries sind Anfragen, die Informationen aus dem System abrufen, ohne den Zustand zu verändern. Sie können ebenfalls synchron oder asynchron sein:

- **Direct Queries**: Synchrone Abfragen, die sofort Daten zurückliefern.
  ```csharp
  Result<T> ExecuteQuery<T>(QueryParams parameters);
  ```
- **Initiate Queries**: Asynchrone Abfragen, deren Ergebnisse später bereitgestellt werden.
  ```csharp
  Task<Result> InitiateQuery(QueryParams parameters);
  ```

### Event-Driven APIs

Um die Interaktionen innerhalb der Plattform effizient und reaktionsfähig zu gestalten, unterstützt die API auch ein eventgesteuertes Modell. Dieses Modell ermöglicht es Modulen, Events zu generieren, die von der Engine oder anderen Modulen abonniert und verarbeitet werden können, um eine lose Kopplung und hohe Reaktionsfähigkeit des Gesamtsystems zu gewährleisten.

### API-Dokumentation und Standards

Eine umfassende Dokumentation jeder API ist unerlässlich, um eine korrekte und effiziente Nutzung der bereitgestellten Funktionalitäten sicherzustellen. Die Dokumentation sollte detaillierte Informationen zu den erwarteten Parametern, den Rückgabewerten, und dem Verhalten bei Fehlern für jede Art von Command oder Query enthalten. Dies stellt sicher, dass Entwickler klare und präzise Anleitungen haben, wie sie die APIs nutzen können, um eine nahtlose Integration und optimale Leistung zu erreichen.

## Lebenszyklusmanagement durch .NET's Hosted Service

Ein effizientes Management des Lebenszyklus für alle Module innerhalb der Kurmann.Videoschnitt.Engine ist entscheidend für die Aufrechterhaltung einer stabilen und zuverlässigen Plattform. Um dieses Ziel zu erreichen, setzen wir auf die Funktionalitäten von `.NET's Hosted Service`. Dieser Ansatz bietet eine robuste und standardisierte Methode, um den Start, die Ausführung und die Beendigung von Diensten innerhalb der Anwendung zu steuern.

### Integration in die Engine

Jedes Modul in der Kurmann.Videoschnitt.Engine implementiert das `IHostedService`-Interface, welches spezielle Methoden zur Verwaltung des Lebenszyklus bereitstellt. Diese Schnittstelle erlaubt es, Module als Dienste zu behandeln, die durch das .NET Core Hosting Framework verwaltet werden.

#### StartAsync und StopAsync Methoden

Die `IHostedService`-Schnittstelle definiert zwei Hauptmethoden, die für das Lebenszyklusmanagement von Modulen entscheidend sind:

- **StartAsync**: Diese Methode wird aufgerufen, wenn die Anwendung startet. Hier können Module ihre Initialisierungslogik durchführen, Ressourcen allokieren und notwendige Startkonfigurationen einstellen. Dies ist der ideale Ort für Module, um Verbindungen zu Datenbanken herzustellen, Netzwerkressourcen zu initialisieren oder einfach ihre interne Zustände vorzubereiten.
  ```csharp
  public Task StartAsync(CancellationToken cancellationToken)
  {
      // Initialisierungslogik hier
      return Task.CompletedTask;
  }
  ```

- **StopAsync**: Diese Methode wird aufgerufen, wenn die Anwendung eine ordnungsgemäße Beendigung durchführt. Hier können Module ihre Bereinigungslogik durchführen, offene Ressourcen freigeben und sicherstellen, dass alle Daten korrekt gespeichert sind, bevor die Anwendung vollständig herunterfährt.
  ```csharp
  public Task StopAsync(CancellationToken cancellationToken)
  {
      // Bereinigungslogik hier
      return Task.CompletedTask;
  }
  ```

### Vorteile des Hosted Service

- **Konsistenz**: Das Verhalten der Module während des Startens und Beendens wird durch das .NET Framework standardisiert, was die Konsistenz über alle Module hinweg sicherstellt.
- **Zuverlässigkeit**: Durch die Verwendung standardisierter Methoden reduziert sich das Risiko von Fehlern bei der Implementierung des Lebenszyklusmanagements.
- **Einfachheit**: Entwickler müssen sich nicht um die Details der Lebenszyklussteuerung kümmern, sondern können sich auf die Kernlogik der Module konzentrieren.

### Beispiel für ein Modul als Hosted Service

Hier ist ein Beispiel, wie ein typisches Modul als `IHostedService` implementiert werden könnte:

```csharp
public class VideoProcessingService : IHostedService
{
    public Task StartAsync(CancellationToken cancellationToken)
    {
        // Initialisierungslogik für das Videoverarbeitungsmodul
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        // Bereinigungslogik für das Videoverarbeitungsmodul
        return Task.CompletedTask;
    }
}
```

## Konfiguration

Die Konfiguration der Kurmann.Videoschnitt.Engine ist entscheidend für die Anpassung und Skalierung der Plattform, um unterschiedlichen Anforderungen gerecht zu werden. Das Konzept der Konfigurationssektionen spielt dabei eine zentrale Rolle, da es eine klare Trennung und Organisation der Einstellungen nach fachlichen Domänen ermöglicht.

### Konfigurationsmanagement mit Sections

Sections in der Konfiguration dienen dazu, Einstellungen thematisch zu gruppieren. Jede Section repräsentiert eine Fachdomäne, wie beispielsweise die "LocalMediaLibrary", und enthält alle relevanten Einstellungen. Diese Herangehensweise fördert die Modularität und Wartbarkeit der Konfiguration.

#### Definition der Sections

In der Konfigurationsdatei `appsettings.json` könnten die Sections wie folgt definiert sein:

```json
{
  "LocalMediaLibrary": {
    "LibraryPath": "/path/to/media/library",
    "CacheSize": 1024
  }
}
```

#### Verwendung von Umgebungsvariablen

In Docker- oder anderen containerisierten Umgebungen ist es üblich, Konfigurationen über Umgebungsvariablen zu steuern. Dies erlaubt eine flexible und sichere Handhabung von Konfigurationswerten. Für die obige Section könnten die entsprechenden Umgebungsvariablen so gesetzt werden:

- `LocalMediaLibrary__LibraryPath="/path/to/media/library"`
- `LocalMediaLibrary__CacheSize="1024"`

Diese Variablen würden typischerweise in der Docker-Konfiguration oder durch das Container-Orchestrierungssystem gesetzt:

```dockerfile
ENV LocalMediaLibrary__LibraryPath="/path/to/media/library"
ENV LocalMediaLibrary__CacheSize="1024"
```

### Implementierung mittels Options Pattern

Das Options Pattern in .NET ermöglicht eine starke Typisierung der Konfigurationseinstellungen. Durch das Definieren von Konfigurationsklassen für jede Section und die Bindung dieser Klassen an die entsprechenden Sections in der Konfigurationsdatei oder die Umgebungsvariablen kann eine sichere und übersichtliche Verwaltung der Einstellungen erreicht werden.

#### Konfigurationsklasse

Für jede Section wird eine Klasse definiert, die die Einstellungen als Eigenschaften enthält. Beispiel für die `LocalMediaLibrary`:

```csharp
public class LocalMediaLibrarySettings
{
    public string LibraryPath { get; set; }
    public int CacheSize { get; set; }
}
```

#### Bindung der Konfigurationsklassen

Die Bindung erfolgt im Startup der Anwendung, wo die `Configure`-Methode des `IServiceCollection` verwendet wird, um die Konfigurationsklassen mit den entsprechenden Sections zu verknüpfen:

```csharp
public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddEngine(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.Configure<LocalMediaLibrarySettings>(configuration.GetSection("LocalMediaLibrary"));
        // Weitere Services hinzufügen
        return services;
    }
}
```

### Vorteile dieser Strategie

- **Klarheit und Ordnung**: Durch die Verwendung von Sections wird die Konfiguration klar und übersichtlich gehalten.
- **Flexibilität und Sicherheit**: Umgebungsvariablen bieten eine flexible und sichere Methode zur Konfigurationsverwaltung, besonders in Produktionsumgebungen.
- **Typsicherheit**: Das Options Pattern stellt sicher, dass die Konfigurationswerte korrekt typisiert sind, was die Fehleranfälligkeit reduziert.

Diese Strategie gewährleistet, dass die Konfiguration der Kurmann.Videoschnitt.Engine effizient verwaltet und an sich ändernde Anforderungen angepasst werden kann, während gleichzeitig eine robuste und fehlerresistente Plattform für die Videobearbeitung geboten wird.

#### Handling von Arrays in Umgebungsvariablen

Die Handhabung von Arrays in Umgebungsvariablen erfordert weiterhin die Verwendung des doppelten Unterstrichs (`__`) für die Indizierung, um eine klare Struktur und eine korrekte Interpretation der Daten zu gewährleisten. Hier ein Beispiel, wie dies in der Praxis umgesetzt wird:

```plaintext
MediaFileWatcher___WatchDirectories__0=/pfad/zu/verzeichnis1
MediaFileWatcher__WatchDirectories__1=/pfad/zu/verzeichnis2
```

Die Neugestaltung der Konfigurationsstrategie der Kurmann.Videoschnitt.Engine trägt zur effizienten Skalierung und Anpassung an sich ändernde Anforderungen bei und gewährleistet gleichzeitig eine robuste und fehlerresistente Plattform für die Videobearbeitung.

## Methode zur Dateisystemüberwachung

### Ausgangssituation

Du hast eine .NET-Anwendung, die in einem Docker-Container auf einer Synology NAS läuft, und möchtest das Dateisystem überwachen, um Dateiänderungen zu erkennen. Auf Windows und macOS funktionierte der `FileSystemWatcher` tadellos, jedoch gab es Probleme in der Linux-Umgebung des Synology NAS.

### Diskussion der Lösungsansätze

1. **Erhöhung der Inotify-Limits**:
    - Erhöhung der Inotify-Watch-Limits auf dem Synology NAS, um mehr Dateien und Verzeichnisse überwachen zu können.
    - Diese Lösung könnte die Performance-Probleme teilweise lösen, aber es bestehen weiterhin potenzielle Einschränkungen und Probleme mit Inotify in Docker-Containern.

2. **Polling-Ansatz**:
    - Implementierung eines Polling-Ansatzes, der regelmäßig das Dateisystem scannt, um Änderungen zu erkennen.
    - Obwohl dieser Ansatz zuverlässig ist, kann er bei großen Verzeichnisstrukturen erhebliche Systemressourcen beanspruchen, besonders bei kurzen Intervallen.

3. **CLI zum manuellen Antreiben von Verzeichnisscans**:
    - Verwendung von CLI-Befehlen, um das Scannen des Dateisystems manuell anzustoßen.
    - Diese Methode erlaubt eine gezielte Überwachung ohne kontinuierliches Polling, könnte jedoch umständlich sein, wenn viele manuelle Eingriffe erforderlich sind.

4. **HTTP-Endpoints zur Auslösung von Scans**:
    - Implementierung von HTTP-Endpoints in der .NET-Anwendung, die externe Systeme oder Skripte nutzen können, um den Scan zu starten.
    - Diese Lösung ermöglicht eine flexible und ereignisbasierte Überwachung, bei der externe Programme (wie ein Videoschnittprogramm) nach Abschluss eines Exports einen Scan auslösen können.

5. **Synology API zur Überwachung und Benachrichtigung**:
    - Verwendung der Synology File Station API, um Dateiänderungen zu überwachen und Benachrichtigungen an die Docker-Anwendung zu senden.
    - Dieser Ansatz nutzt die vorhandenen Tools und Dienste der Synology NAS, um Änderungen effizient zu erkennen und zu verarbeiten.

### Entscheidungsfindung

Nach der Diskussion der verschiedenen Ansätze wurde entschieden, einen hybriden Ansatz zu wählen, der folgende Aspekte kombiniert:

- **Periodisches Polling**: Das Dateisystem wird alle Stunde gescannt, was eine vertretbare Belastung für die Systemressourcen darstellt.
- **HTTP-Endpoint für Echtzeit-Scans**: Implementierung einer Minimal API in der .NET-Anwendung, um Verzeichnisscans manuell anzustoßen. Diese API kann von externen Systemen (z.B. dem Videoschnittprogramm) genutzt werden, um sofortige Scans nach Bedarf auszulösen.
- **Flexibilität durch Umgebungsvariablen**: Das Zeitintervall für das Polling kann manuell über Umgebungsvariablen angepasst werden, um auf unterschiedliche Last- und Anwendungsanforderungen flexibel reagieren zu können.

### Gründe für die Entscheidung

- **Performance**: Ein stündliches Polling-Intervall stellt sicher, dass die Systemressourcen nicht übermäßig belastet werden, während regelmäßige Überprüfungen weiterhin stattfinden.
- **Flexibilität**: Durch die Möglichkeit, Scans manuell anzustoßen, kann auf spezifische Ereignisse (wie das Abschließen eines Videoexports) sofort reagiert werden, ohne auf das nächste Polling-Intervall warten zu müssen.
- **Einfache Implementierung und Wartung**: Der hybride Ansatz ist relativ einfach zu implementieren und zu warten, da er die Vorteile von periodischem Polling und ereignisbasierter Überwachung kombiniert.

Durch diese Lösung wird ein guter Kompromiss zwischen Systemressourcen und Reaktionszeit erreicht, was die Überwachung des Dateisystems effizient und flexibel gestaltet.

## Mitwirken

1. **Issue einreichen**: Wenn Sie einen Fehler finden oder eine Funktion anfordern möchten, eröffnen Sie ein Issue im GitHub-Repository.
2. **Pull Requests**: Wenn Sie eine direkte Änderung oder Ergänzung vorschlagen möchten, senden Sie einen Pull Request mit einer klaren Beschreibung Ihrer Änderungen.

## Lizenz

Dieses Projekt ist unter der Apache-2.0-Lizenz lizenziert. Weitere Details finden Sie in der Datei [LICENSE](LICENSE) im GitHub-Repository. Diese Lizenz ermöglicht es sowohl kommerziellen als auch nicht-kommerziellen Nutzern, die Software frei zu verwenden, zu modifizieren und weiterzuverbreiten, unter der Bedingung, dass Änderungen und Erweiterungen unter der gleichen Lizenz bleiben.

## Kontakt

Falls Sie Fragen haben oder Unterstützung benötigen, eröffnen sie bitte ein Issue im GitHub-Repository.
