namespace Core.Events.Parameters;

public interface IAddParameter
{
    public Guid CorrelationId { get; set; }
    public Guid SessionId { get; set; }
    public string Name { get; set; }
    public string ParameterTypeId { get; set; }
    public bool IsActive { get; set; }
}

public class AddParameter : IAddParameter
{
    public Guid CorrelationId { get; set; }
    public Guid SessionId { get; set; }
    public string Name { get; set; }
    public string ParameterTypeId { get; set; }
    public bool IsActive { get; set; }

}