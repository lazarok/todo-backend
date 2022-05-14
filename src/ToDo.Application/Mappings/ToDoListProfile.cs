using AutoMapper;
using ToDo.Application.Handlers.ToDoLists.Commands.CreateToDoList;
using ToDo.Application.Mappings.Base;
using ToDo.Application.Models.Dtos;
using ToDo.Domain.Entities;

namespace ToDo.Application.Mappings;

public class ToDoListProfile : IMapFrom<ToDoList>
{
    public void Mapping(Profile profile)
    {
        profile.CreateMap<ToDoList, ToDoListDto>();
        profile.CreateMap<CreateToDoListCommand, ToDoList>();
    }
}