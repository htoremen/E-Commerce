namespace Todo.Application.Parameters.Command.AddParameters
{
    public class AddParameterRequest
    {
        public string Name { get; set; }
        public string ParameterTypeId { get; set; }
        public bool IsActive { get; set; }
    }
}