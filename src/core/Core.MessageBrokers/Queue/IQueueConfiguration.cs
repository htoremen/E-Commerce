using Core.MessageBrokers.Enums;

namespace Core.MessageBrokers
{
    public interface IQueueConfiguration
    {
        public Dictionary<QueueName, string> Names { get; set; }
    }
}
