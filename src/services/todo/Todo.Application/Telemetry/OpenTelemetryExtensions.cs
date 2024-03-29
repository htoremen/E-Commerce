﻿using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Todo.Application.Telemetry
{
    public static class OpenTelemetryExtensions
    {
        public static string Local { get; }
        public static string Kernel { get; }
        public static string Framework { get; }
        public static string ServiceName { get; }
        public static string ServiceVersion { get; }

        static OpenTelemetryExtensions()
        {
            // Local = "Cargo.Todo.Application";
            Kernel = Environment.OSVersion.VersionString;
            Framework = RuntimeInformation.FrameworkDescription;
            ServiceName = typeof(OpenTelemetryExtensions).Assembly.GetName().Name!;
            ServiceVersion = typeof(OpenTelemetryExtensions).Assembly.GetName().Version!.ToString();
        }

        public static ActivitySource CreateActivitySource() => new ActivitySource(ServiceName, ServiceVersion);
    }
}