using AutoMapper;
using ToDo.Application.Mappings;
using ToDo.Domain.Entities;
using ToDo.Domain.Enums;

namespace ToDo.Application.Handlers.ToDoItems.Queries.GetToDoItemById;

public class GetToDoItemByIdMapper : IMapFrom<ToDoItem>
{
    public void Mapping(Profile profile)
    {
        profile.CreateMap<ToDoItem, GetToDoItemByIdDto>();
    }
}