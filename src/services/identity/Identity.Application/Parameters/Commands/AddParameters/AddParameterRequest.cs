namespace Identity.Application.Parameters.Command.AddParameters
{
    public class AddParameterRequest
    {
        public string ParameterName { get; set; }
        public string ParameterTypeId { get; set; }
        public bool IsActive { get; set; }
    }
}
