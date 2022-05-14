using Microsoft.AspNetCore.Mvc;
using ToDo.Application.Errors;
using ToDo.Application.Handlers.ToDoLists.Commands.CreateToDoList;
using ToDo.Application.Handlers.ToDoLists.Commands.DeleteToDoList;
using ToDo.Application.Handlers.ToDoLists.Commands.UpdateToDoList;
using ToDo.Application.Handlers.ToDoLists.Queries.GetAllToDoList;
using ToDo.Application.Handlers.ToDoLists.Queries.GetToDoListById;
using ToDo.Application.Models.Dtos;
using ToDo.Application.Pagination;
using ToDo.Application.QueryFilters;

namespace ToDo.Api.Controllers.v1;

[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/todo-lists")]
public class ToDoListController : BaseApiController
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [ProducesResponseType(typeof(ToDoListDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(Guid id)
    {
        return Ok(await Mediator.Send(new GetToDoListByIdQuery() { Id = id }));
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="filter"></param>
    /// <returns></returns>
    [ProducesResponseType(typeof(ListItems<ToDoListDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] GetAllToDoListFilter filter)
    {
        return Ok(await Mediator.Send(new GetAllToDoListQuery()
        {
            Filters = filter
        }));
    }

    /// <summary>
    /// Add new ToDoList.
    /// </summary>
    /// <param name="command"></param>
    /// <returns>Created ToDoList</returns>
    [ProducesResponseType(typeof(ToDoListDto), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
    [HttpPost]
    public async Task<IActionResult> Post(CreateToDoListCommand command)
    {
        return Ok(await Mediator.Send(command));
    }
    
    /// <summary>
    /// Add new ToDoList.
    /// </summary>
    /// <param name="command">Parameter</param>
    /// <returns>Created ToDoList</returns>
    [ProducesResponseType(typeof(ToDoListDto), StatusCodes.Status202Accepted)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
    [HttpPut("{id}")]
    public async Task<IActionResult> Put([FromRoute] Guid id, UpdateToDoListDto update)
    {
        return Ok(await Mediator.Send(new UpdateToDoListCommand
        {
            Id = id,
            Title = update.Title,
            Description = update.Description
        }));
    }
    
    /// <summary>
    ///
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    [ProducesResponseType(typeof(ToDoListDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        return Ok(await Mediator.Send(new DeleteToDoListCommand()
        {
            Id = id
        }));
    }  
}