using Identity.Application.Users;

namespace Identity.Application;

public class AutoMapProfile : Profile
{
    public AutoMapProfile()
    {
        CreateMap<LoginRequest, LoginCommand>();
        CreateMap<CreateUserRequest, CreateUserCommand>();
    }
}
