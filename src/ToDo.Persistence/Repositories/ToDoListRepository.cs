using ToDo.Application.Interfaces;
using ToDo.Application.Repositories;
using ToDo.Domain.Entities;
using ToDo.Persistence.Base;

namespace ToDo.Persistence.Repositories;

public class ToDoListRepository : BaseRepository<ToDoList>,IToDoListRepository
{
    private readonly IToDoDbContext _context;

    public ToDoListRepository(IToDoDbContext context) : base(context)
    {
        _context = context;
    }
}