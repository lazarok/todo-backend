using Microsoft.EntityFrameworkCore;
using ToDo.Application.Repositories;
using ToDo.Domain.Entities;

namespace ToDo.Application.Interfaces;

public interface IToDoUnitOfWork : IDbContext
{
    IToDoItemRepository ToDoItemRepository { get; }
    IToDoListRepository ToDoListRepository { get; }
}