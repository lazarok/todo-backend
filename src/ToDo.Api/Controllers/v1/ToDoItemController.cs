using System.ComponentModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using ToDo.Application.Errors;
using ToDo.Application.Handlers.ToDoItems.Queries.GetToDoItemById;

namespace ToDo.Api.Controllers.v1;

[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/todo-items")]
public class ToDoItemController : BaseApiController
{
    /// <summary>
    /// Get a specific TodoItem.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [ProducesResponseType(typeof(GetToDoItemByIdDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> Get(Guid id)
    {
        return Ok(await Mediator.Send(new GetToDoItemByIdQuery(id)));
    }
}