using Microsoft.EntityFrameworkCore;
using ToDo.Domain.Entities;

namespace ToDo.Application.Interfaces;

public interface IToDoDbContext : IDbContext
{
    public DbSet<ToDoItem> ToDoItems { get; set; }
    public DbSet<ToDoList> ToDoLists { get; set; }
}