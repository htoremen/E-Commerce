using MediatR;
using Microsoft.AspNetCore.Mvc;
using Order.Application.Common.Models;

namespace Order.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TodoController : ApiControllerBase
{
    [HttpGet]
    [Route("")]
    public async Task<PaginatedList<TodoItemBriefDto>> GetTodoItemsWithPagination(ISender sender, [AsParameters] GetTodoItemsWithPaginationQuery query)
    {
        return await sender.Send(query);
    }
}
