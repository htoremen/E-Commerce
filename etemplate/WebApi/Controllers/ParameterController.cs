using Application.Parameters.Command.AddParameters;
using Domain;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ParameterController : ApiControllerBase
{
    [HttpPost]
    [Route("add-parameter")]
    public async Task<GenericResponse<AddParameterResponse>> AddParameter(AddParameterRequest request)
    {
       return await Mediator.Send(new AddParameterCommand { Data = request });
    }
}