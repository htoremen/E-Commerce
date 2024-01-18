using MassTransit;

namespace Saga.Persistence;

public class StateInstance : SagaStateMachineInstance
{
    public Guid CorrelationId { get; set; }
    public Guid SessionId { get; set; }
    public string CurrentState { get; set; }

    public Guid CustomerId { get; set; }
    public Guid UserId { get; set; }
    public Guid ProductId { get; set; }
    public DateTime CreatedOn { get; set; }
    public string Token { get; set; }
}