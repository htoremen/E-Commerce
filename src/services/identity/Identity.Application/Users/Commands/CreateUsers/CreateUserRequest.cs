namespace Identity.Application.Users;

public class CreateUserRequest
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
   // public List<UserRegisterStageModel>? UserRegisterStages { get; set; }
}

public class UserRegisterStageModel
{
    public string ParameterId { get; set; }
    public string ParameterTypeId { get; set; }
    public string ParameterStageId { get; set; }
}