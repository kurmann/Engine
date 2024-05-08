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

Dieses Kapitel zeigt, wie durch die Verwendung von .NET's Hosted Services ein effizientes Lebenszyklusmanagement für Module innerhalb der Kurmann.Videoschnitt.Engine realisiert wird, was zu einer verbesserten Stabilität und Zuverlässigkeit der Plattform führt.

## Konfiguration
Details zur Konfiguration der Engine und der einzelnen Module.

### Umgebungsvariablen und Einstellungen
Wie Umgebungsvariablen und spezifische Einstellungen gehandhabt werden.

## Dokumentation und Standards
Informationen zur Verfügbarkeit und Bedeutung der Dokumentation und Coding-Standards.

## Mitwirken
Anleitung, wie man am Projekt mitwirken kann, inklusive Links zu Issues und Pull Requests.

## Lizenz
Informationen zur Lizenzierung des Projekts.

## Kontakt
Kontaktinformationen und wie man Unterstützung erhalten kann.
