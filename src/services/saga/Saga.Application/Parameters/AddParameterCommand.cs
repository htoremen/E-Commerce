using Core.Events.Parameters;

namespace Saga.Application.Parameters;

public class AddParameterCommand : IAddParameter
{
    public AddParameterCommand(Guid correlationId)
    {
        CorrelationId = correlationId;
    }

    public Guid CorrelationId { get; set; }
    public Guid SessionId { get; set; }
    public string Name { get; set; }
    public string ParameterTypeId { get; set; }
    public bool IsActive { get; set; }
}
