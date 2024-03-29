﻿namespace Core.MessageBrokers.MessageBrokers;

public static class RabbitMQStaticValues
{
    public static int RetryCount { get; set; }
    public static int RetryTimeInterval { get; set; }
    public static int PrefetchCount { get; set; }
    public static int TrackingPeriod { get; set; }
    public static int TripThreshold { get; set; }
    public static int ActiveThreshold { get; set; }
    public static int ResetInterval { get; set; }
    public static string Password { get; set; }
    public static string HostName { get; set; }
    public static string VirtualHost { get; set; }
    public static string ConnectionString { get; set; }
}
