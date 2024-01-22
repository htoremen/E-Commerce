using System.Diagnostics;

namespace Todo.Application.Telemetry
{
    public static class ConsumerActivitySource
    {
        public static readonly ActivitySource Source = OpenTelemetryExtensions.CreateActivitySource();
    }
}