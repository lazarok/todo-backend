using ToDo.Application.Interfaces.Repositories.Common;
using ToDo.Domain.Entities;

namespace ToDo.Application.Repositories;

public interface IToDoItemRepository : IRepository<ToDoItem>
{
    
}