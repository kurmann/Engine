# Kurmann.Videoschnitt.Engine

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

Die Konfiguration in der Kurmann.Videoschnitt.Engine spielt eine entscheidende Rolle bei der Anpassung und Skalierung der Plattform, um unterschiedlichen Anforderungen gerecht zu werden. Die Engine setzt auf hohe Modularität und Flexibilität in der Konfiguration der verschiedenen Bereiche ihrer Architektur, wobei das Options-Pattern von .NET genutzt wird, um bereichsspezifische Einstellungen zu ermöglichen.

### Konfigurationsmanagement

Die Verwaltung der Konfiguration erfolgt primär über Umgebungsvariablen, die es erlauben, die Einstellungen je nach Deployment-Umgebung einfach zu ändern. Durch die Trennung der Konfigurationseinstellungen in dedizierte Abschnitte für jedes Modul, kann die Engine flexibel auf die Bedürfnisse jedes Bereichs eingehen, ohne dass sich Einstellungen gegenseitig beeinflussen.

#### Unabhängige Konfiguration der Bereiche

Jeder Bereich der Videobearbeitungsplattform besitzt eine dedizierte Konfigurationssektion, die spezifisch auf seine Anforderungen zugeschnitten ist. Dies stellt sicher, dass die Konfigurationen isoliert voneinander bleiben und vereinfacht die Wartung und Erweiterung der Plattform.

#### Beispiel für bereichsspezifische Umgebungsvariablen:

```plaintext
KurmannVideoschnitt_MediaLibraryOptions__LibraryPath=/path/to/media/library
KurmannVideoschnitt_VideoProcessingOptions__DefaultCodec=HEVC
KurmannVideoschnitt_VideoProcessingOptions__Resolution=4K
```

### Integration in die Engine

Zur Laufzeit werden diese Konfigurationen durch das .NET Core DI-System injiziert und in die entsprechenden Modulkomponenten geladen. Dies geschieht über das `IServiceCollection`-Framework, das eine starke Typisierung und einfache Verwaltung der Konfigurationsdaten ermöglicht.

```csharp
services.Configure<MediaLibraryOptions>(Configuration.GetSection("MediaLibraryOptions"));
services.Configure<VideoProcessingOptions>(Configuration.GetSection("VideoProcessingOptions"));
```

### Best Practices für die Konfiguration

- **Klare Vertragsdefinition**: Jeder Konfigurationsbereich sollte durch eine klare und gut definierte Schnittstelle repräsentiert werden, um die Integration und das Management der Konfigurationsdaten zu vereinfachen.
- **Einsatz von Umgebungsvariablen für übergreifende Einstellungen**: Für allgemeine oder sicherheitssensible Konfigurationen sollten Umgebungsvariablen verwendet werden, um die Flexibilität und Sicherheit zu erhöhen.
- **Konsistente Namenskonventionen**: Die Namen der Konfigurationsbereiche und ihrer Schlüssel sollten sorgfältig gewählt werden, um Klarheit und Konsistenz zu gewährleisten.

#### Handling von Arrays in Umgebungsvariablen

Das Handling von Arrays in Umgebungsvariablen benötigt besondere Aufmerksamkeit, insbesondere bei der Verwendung von indizierten Werten. Hier ein Beispiel, wie die `WatchDirectories` in Umgebungsvariablen definiert werden können:

```plaintext
KurmannVideoschnitt_WatchDirectories__0=/pfad/zu/verzeichnis1
KurmannVideoschnitt_WatchDirectories__1=/pfad/zu/verzeichnis2
```

Die Verwendung des doppelten Unterstrichs (`__`) ist essenziell, um hierarchische Datenstrukturen in Umgebungsvariablen korrekt zu repräsentieren und sicherzustellen, dass die Konfigurationseinstellungen korrekt von der .NET-Konfigurations-API gelesen und verarbeitet werden können.

## Mitwirken

1. **Issue einreichen**: Wenn Sie einen Fehler finden oder eine Funktion anfordern möchten, eröffnen Sie ein Issue im GitHub-Repository.
2. **Pull Requests**: Wenn Sie eine direkte Änderung oder Ergänzung vorschlagen möchten, senden Sie einen Pull Request mit einer klaren Beschreibung Ihrer Änderungen.

## Lizenz

Dieses Projekt ist unter der Apache-2.0-Lizenz lizenziert. Weitere Details finden Sie in der Datei [LICENSE](LICENSE) im GitHub-Repository. Diese Lizenz ermöglicht es sowohl kommerziellen als auch nicht-kommerziellen Nutzern, die Software frei zu verwenden, zu modifizieren und weiterzuverbreiten, unter der Bedingung, dass Änderungen und Erweiterungen unter der gleichen Lizenz bleiben.

## Kontakt

Falls Sie Fragen haben oder Unterstützung benötigen, zögern Sie nicht, ein Issue im GitHub-Repository zu eröffnen oder uns direkt zu kontaktieren. Unsere Kontaktinformationen finden Sie auf der GitHub-Projektseite.
