using Core.MessageBrokers;

namespace Saga.Application;

public class AppSettings
{
    public MessageBrokerOptions MessageBroker { get; set; }
    public ConnectionStrings ConnectionStrings { get; set; }
}
public class ConnectionStrings
{
    public string ConnectionString { get; set; }
    public string Monitoring { get; set; }
}
