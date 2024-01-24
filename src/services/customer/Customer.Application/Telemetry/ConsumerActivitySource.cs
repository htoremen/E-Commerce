using System.Diagnostics;

namespace Customer.Application.Telemetry
{
    public static class ConsumerActivitySource
    {
        public static readonly ActivitySource Source = OpenTelemetryExtensions.CreateActivitySource();
    }

}
