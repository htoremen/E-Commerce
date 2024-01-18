namespace Core.MessageBrokers;
public interface IQueueConfiguration
{
    public Dictionary<QueueName, string> Names { get; set; }
}

public class QueueConfiguration : IQueueConfiguration
{
    public Dictionary<QueueName, string> Names { get; set; }
}
