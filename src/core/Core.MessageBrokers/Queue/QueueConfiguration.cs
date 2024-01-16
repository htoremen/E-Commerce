namespace Core.MessageBrokers
{
    public class QueueConfiguration : IQueueConfiguration
    {
        public Dictionary<QueueName, string> Names { get; set; }
    }
}
