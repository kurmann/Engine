# Videoschnitt-Steuereinheit

Die Kurmann.Videoschnitt.Engine, der zentralen Steuereinheit für die Videobearbeitungsplattform von Kurmann, ist das Herzstück des modularen Systems und koordiniert die verschiedenen spezialisierten Module, die zusammenarbeiten, um umfassende Lösungen im Bereich Videoschnitt zu bieten.

## Über die Engine

Die Kurmann.Videoschnitt.Engine ist entworfen, um eine robuste und flexible Plattform bereitzustellen, die die Integration verschiedener Videobearbeitungsmodule ermöglicht. Diese Engine ist nicht nur eine Laufzeitumgebung, sondern orchestriert auch die Ausführung von Aufgaben, verwaltet den Datenfluss zwischen Modulen und sorgt für die Einhaltung der Geschäftslogiken über das gesamte System.

### Kernfunktionen

- **Modulintegration**: Die Engine verwendet `IServiceCollection` für die flexible und konfigurierbare Integration von Modulen. Jedes Modul kann spezifische Funktionen wie Rendering, Schnitt, Effekte und mehr handhaben.

- **Businesslogik-Orchestrierung**: Neben der technischen Koordination der Module steuert die Engine auch die Geschäftslogik, die die Entscheidungsfindung innerhalb des gesamten Systems leitet.

- **Workflow-Management**: Die Engine ist verantwortlich für die Steuerung der Workflows, die notwendig sind, um Videoprojekte von Anfang bis Ende zu managen.

## Architektur allgemein

Die Engine ist als modularer Monolith konzipiert, was bedeutet, dass sie zwar aus einzelnen, unabhängigen Modulen besteht, diese jedoch innerhalb eines einzigen, einheitlichen Prozesses laufen. Dieser Ansatz kombiniert die Einfachheit und Effizienz eines monolithischen Designs mit der Flexibilität und Skalierbarkeit modularer Komponenten.

## API-Mechanismus der Module

Jedes Modul in der Kurmann.Videoschnitt.Engine ist dafür ausgelegt, über eine gut definierte API mit der zentralen Engine zu kommunizieren. Diese Schnittstellen sind entscheidend für die effiziente und fehlerfreie Interaktion innerhalb des Gesamtsystems. Die folgenden Richtlinien sollen Entwicklern helfen, ihre Module so zu implementieren, dass sie nahtlos in die Engine integriert werden können.

### Allgemeine Prinzipien

1. **Interface-Definition**: Jedes Modul muss ein klar definiertes Interface implementieren, das seine Funktionen und die Art und Weise, wie es mit der Engine kommuniziert, beschreibt. Diese Interfaces sollten spezifische Methoden für die Aufgaben enthalten, die das Modul ausführen kann.

2. **Dependency Injection**: Module sollten so gestaltet sein, dass sie ihre Abhängigkeiten über Konstruktoren oder öffentliche Eigenschaften injizieren lassen können. `IServiceCollection` wird genutzt, um diese Abhängigkeiten zur Laufzeit bereitzustellen und zu verwalten.

3. **Callback-Mechanismen**: Um asynchrone Operationen zu unterstützen, sollten Module Callbacks oder ähnliche Mechanismen verwenden, um die Ergebnisse von Operationen zurück an die Engine zu kommunizieren.

4. **Fehlerbehandlung**: Jedes Modul sollte robuste Fehlerbehandlungsmechanismen implementieren, um sicherzustellen, dass Fehler ordnungsgemäß erfasst und behandelt werden, ohne dass sie das Gesamtsystem beeinträchtigen.

### Command und Query Trennung (CQRS)

Die API-Struktur jedes Moduls basiert auf dem Prinzip der Command Query Responsibility Segregation (CQRS), das eine klare Trennung zwischen Befehlen (Commands), die den Systemzustand ändern, und Abfragen (Queries), die Daten zurückgeben, vorsieht:

- **Initiate Commands**: Methoden, die eine Veränderung oder eine Aktion im System bewirken und lediglich bestätigen, dass der Befehl angenommen wurde. Die endgültigen Ergebnisse dieser Befehle werden durch Events kommuniziert.

- **Direct Commands**: Synchrone Commands, die unmittelbare Ergebnisse liefern und direkt über die Rückgabewerte der Funktionen kommuniziert werden.

- **Initiate Queries**: Asynchrone Abfragen, deren Ergebnisse nicht sofort verfügbar sind. Diese werden ähnlich wie Initiate Commands behandelt, wobei die Ergebnisse später durch Events zur Verfügung gestellt werden.

- **Direct Queries**: Synchrone Abfragen, die unmittelbar Ergebnisse in Form von `Result<T>` liefern, wobei `T` den Typ der angeforderten Daten darstellt.

### Beispielhafte API-Struktur

Hier ist ein Beispiel für eine mögliche API-Struktur eines Moduls:

```csharp
public interface IVideoProcessingModule
{
    Task<Result> InitiateProcessCommand(CommandParams parameters);
    Result<IEnumerable<VideoData>> DirectFetchQuery(QueryParams query);
}
```

In diesem Beispiel:

- `CommandParams` und `QueryParams` sind spezifische Parameterklassen für Commands und Queries.
- `Result` und `Result<T>` sind Typen, die verwendet werden, um das Ergebnis der Operationen sofort oder nach Abschluss der asynchronen Verarbeitung durch Events zurückzumelden.

### Integration in die Engine

- **Registrierung**: Module müssen sich bei ihrer Initialisierung selbst bei der Engine registrieren, indem sie ihre Dienste zur `IServiceCollection` hinzufügen.
- **Konfiguration**: Konfigurationseinstellungen für jedes Modul sollten über die Engine zugänglich gemacht werden, idealerweise durch Umgebungsvariablen oder Konfigurationsdateien.
- **Lebenszyklus-Management**: Die Engine sollte die Fähigkeit haben, den Lebenszyklus jedes Moduls zu steuern, einschließlich Starten, Stoppen und Neustarten bei Bedarf.

### Dokumentation und Standards

- Jedes Modul sollte eine umfassende Dokumentation seiner API bereitstellen, die klare Anweisungen zu den erwarteten Parametern, den Rückgabewerten und dem Fehlerverhalten enthält.
- Die Einhaltung von Coding-Standards und Best Practices ist entscheidend, um die Qualität und Wartbarkeit des Gesamtsystems zu gewährleisten.

## Lebenszyklusmanagement durch .NET's Hosted Service

Um ein effizientes Management des Lebenszyklus für alle Module in der Kurmann.Videoschnitt.Engine zu gewährleisten, nutzen wir die Funktionalitäten von `.NET's Hosted Service`. Dieser Ansatz bietet eine robuste und standardisierte Methode, um Start, Ausführung und Beendigung von Diensten innerhalb der Anwendung zu steuern.

### Integration in die Engine

Jedes Modul erbt von `IHostedService`, das spezielle Methoden zur Verwaltung des Lebenszyklus bereitstellt:

- **StartAsync**: Wird aufgerufen, wenn die Anwendung startet. Hier können Module ihre Initialisierung und den Start ihrer Operationen durchführen.
- **StopAsync**: Wird aufgerufen, wenn die Anwendung eine ordnungsgemäße Beendigung durchführt. Module können hier Bereinigungen und notwendige Abschlüsse ihrer Operationen vornehmen.

Durch die Vererbung dieser Schnittstelle erhalten die Module einen strukturierten und zuverlässigen Rahmen, um ihre Lebenszyklusereignisse in Einklang mit dem Gesamtsystem zu handhaben.

### Vorteile des Hosted Service

- **Konsistenz**: Das Verhalten der Module während des Startens und Beendens wird durch das .NET Framework standardisiert, was die Konsistenz über alle Module hinweg sicherstellt.
- **Zuverlässigkeit**: Durch die Verwendung standardisierter Methoden reduziert sich das Risiko von Fehlern bei der Implementierung des Lebenszyklusmanagements.
- **Einfachheit**: Entwickler müssen sich nicht um die Details der Lebenszyklussteuerung kümmern, sondern können sich auf die Kernlogik der Module konzentrieren.

### Beispiel für ein Modul als Hosted Service

```csharp
public class VideoProcessingService : IHostedService
{
    public Task StartAsync(CancellationToken cancellationToken)
    {
        // Initialisierungslogik hier
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        // Bereinigungslogik hier
        return Task.CompletedTask;
    }
}
```

## Konfiguration

Die Kurmann.Videoschnitt.Engine setzt auf hohe Modularität und Flexibilität in der Konfiguration der verschiedenen Bereiche ihrer Architektur. Durch die Nutzung des Options-Patterns von .NET können Bereiche unabhängig konfiguriert werden, ohne dass sich Einstellungen gegenseitig überschreiben oder zentral definiert werden müssen. Dies fördert die Entkopplung innerhalb des Systems und minimiert Abhängigkeiten.

### Unabhängige Konfiguration der Bereiche

Jeder Bereich der Videobearbeitungsplattform besitzt eine dedizierte Konfigurationssektion, die spezifisch auf seine Anforderungen zugeschnitten ist. Dies wird durch separate Konfigurationsabschnitte innerhalb der `appsettings.json` oder anderer Konfigurationsquellen ermöglicht.

#### Beispiel für bereichsspezifische Konfigurationen:

```json
{
  "InfuseMediaLibrary": {
    "LibraryRootPath": "/media/library/root"
  },
  "FinalCutPro": {
    "ExportPath": "/exports/finalcut"
  }
}
```

In diesem Beispiel hat jeder Bereich (InfuseMediaLibrary und FinalCutPro) seine eigene Sektion, was sicherstellt, dass die Konfigurationen isoliert voneinander bleiben.

### Vorteile der isolierten Konfigurationsabschnitte

- **Keine Überschneidungen**: Durch die isolierte Speicherung der Konfigurationsdaten in eigenen Abschnitten wird vermieden, dass Einstellungen anderer Bereiche unbeabsichtigt überschrieben werden.
- **Reduzierte Abhängigkeiten**: Jeder Bereich kann seine Konfigurationsdaten unabhängig von anderen Teilen des Systems beziehen, was die Komplexität reduziert.
- **Skalierbarkeit**: Neue Bereiche können einfach durch Hinzufügen neuer Konfigurationsabschnitte integriert werden, ohne bestehende Bereiche zu beeinflussen.

### Implementierung in .NET

Die Konfigurationen für jeden Bereich werden in der Startup-Konfiguration der Anwendung registriert und über `IServiceCollection` bereitgestellt, wodurch eine starke Typisierung und eine einfache Verwaltung der Konfigurationsdaten ermöglicht wird.

```csharp
services.Configure<MediaLibraryOptions>(Configuration.GetSection("InfuseMediaLibrary"));
services.Configure<FinalCutProOptions>(Configuration.GetSection("FinalCutPro"));
```

Diese Methode gewährleistet, dass jeder Bereich nur auf die für ihn relevanten Einstellungen zugreift und zentrale Abhängigkeiten vermieden werden.

### Best Practices für die Konfiguration

- **Klare Vertragsdefinition**: Jeder Bereich sollte eine klar definierte Schnittstelle für seine Konfigurationsoptionen haben, repräsentiert durch entsprechende Klassen in C#.
- **Einsatz von Umgebungsvariablen für übergreifende Einstellungen**: Für allgemeine oder sicherheitssensible Konfigurationen sollten Umgebungsvariablen verwendet werden, um die Flexibilität und Sicherheit zu erhöhen.
- **Konsistente Namenskonventionen**: Die Namen der Konfigurationsbereiche und deren Schlüssel sollten sorgfältig gewählt werden, um Klarheit und Konsistenz sicherzustellen.

Durch diese strukturierte Herangehensweise an die Konfiguration unterstützt die Kurmann.Videoschnitt.Engine eine effiziente Skalierung und Anpassung an sich ändernde Anforderungen, während sie eine robuste und fehlerresistente Plattform für die Videobearbeitung bietet.

## Beitrag

Beiträge zur Kurmann.Videoschnitt.Engine sind willkommen. Wenn Sie Fehler finden oder neue Features vorschlagen möchten, eröffnen Sie bitte ein Issue im Repository.

## Lizenz

Dieses Projekt ist unter der [MIT Lizenz](LICENSE.md) lizenziert.

## Kontakt

Für weitere Informationen oder Unterstützung kontaktieren Sie mich bitte über [info@kurmann.com](mailto:info@kurmann.com).

Vielen Dank für Ihr Interesse an der Kurmann.Videoschnitt.Engine. Wir hoffen, dass dieses Projekt Ihre Erwartungen erfüllt und zur Verbesserung Ihrer Videobearbeitungsprojekte beiträgt.
