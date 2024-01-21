using System.Diagnostics;

namespace Order.Application.Telemetry
{
    public static class ConsumerActivitySource
    {
        public static readonly ActivitySource Source = OpenTelemetryExtensions.CreateActivitySource();
    }
}