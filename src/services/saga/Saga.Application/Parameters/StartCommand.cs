using Core.Events.Parameters;

namespace Saga.Application.Parameters;

public class StartCommand : IStart
{
    public StartCommand(Guid correlationId)
    {
        CorrelationId = correlationId;
    }

    public Guid CorrelationId { get; set; }
    public Guid SessionId { get; set; }
    public string CurrentState { get; set; }
    public DateTime CreatedOn {  get; set; }
}