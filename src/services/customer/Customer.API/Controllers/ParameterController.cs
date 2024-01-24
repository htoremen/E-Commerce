using Microsoft.AspNetCore.Mvc;

namespace Customer.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParameterController : ApiControllerBase
    {
        [HttpGet]
        [Route("get-parameters")]
        public async Task<GenericResponse<List<GetParameterResponse>>> GetParameters()
        {
            return await Mediator.Send(new GetParameterQuery());
        }

        [HttpPost]
        [Route("add-parameter")]
        public async Task<GenericResponse<AddParameterResponse>> AddParameter(AddParameterRequest request)
        {
            return await Mediator.Send(new AddParameterCommand { Data = request });
        }
    }
}