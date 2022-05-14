using MediatR;
using ToDo.Application.Models.Dtos;
using ToDo.Application.Pagination;
using ToDo.Application.QueryFilters;

namespace ToDo.Application.Handlers.ToDoLists.Queries.GetAllToDoList;

public record GetAllToDoListQuery : IRequest<ListItems<ToDoListDto>>
{
    public GetAllToDoListFilter Filters { get; set; }
}