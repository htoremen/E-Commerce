using Core.Events.Parameters;

namespace Saga.Application.Parameters;

public class CompletedCommand : ICompleted
{
    public CompletedCommand(Guid correlationId)
    {
        CorrelationId = correlationId;
    }

    public Guid CorrelationId { get; set; }
    public Guid SessionId { get; set; }
    public string CurrentState { get; set; }
}
