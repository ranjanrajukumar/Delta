using Serilog.Core;
using Serilog.Events;

namespace Delta.Shared.Logging;

public class CorrelationIdEnricher : ILogEventEnricher
{
    public void Enrich(LogEvent logEvent, ILogEventPropertyFactory factory)
    {
        var correlationId = Guid.NewGuid().ToString();
        logEvent.AddPropertyIfAbsent(
            factory.CreateProperty("CorrelationId", correlationId));
    }
}
