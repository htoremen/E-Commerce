using AutoMapper;
using Identity.Application.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Identity.API.Controllers;

public class UserController : ApiControllerBase
{
    [AllowAnonymous]
    [HttpPost]
    [Route("login")]
    public async Task<GenericResponse<LoginResponse>> Login(LoginRequest model)
    {
        var command = Mapper.Map<LoginCommand>(model);
        command.IpAddress = ipAddress();

        var response = await Mediator.Send(command);
        return response;
    }

    [HttpPost]
    [AllowAnonymous]
    [Route("create-user")]
    public async Task<GenericResponse<CreateUserResponse>> CreateUser(CreateUserRequest model)
    {
        var command = Mapper.Map<CreateUserCommand>(model);
        command.LoginType = Domain.Enums.LoginType.Web;
        var response = await Mediator.Send(command);
        return response;
    }

    private string ipAddress()
    {
        // get source ip address for the current request
        if (Request.Headers.ContainsKey("X-Forwarded-For"))
            return Request.Headers["X-Forwarded-For"];
        else
            return HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
    }
}
