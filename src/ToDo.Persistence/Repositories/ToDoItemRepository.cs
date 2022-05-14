using ToDo.Application.Interfaces;
using ToDo.Application.Repositories;
using ToDo.Domain.Entities;
using ToDo.Persistence.Base;

namespace ToDo.Persistence.Repositories;

public class ToDoItemRepository : BaseRepository<ToDoItem>, IToDoItemRepository
{
    private readonly IToDoDbContext _context;

    public ToDoItemRepository(IToDoDbContext context) : base(context)
    {
        _context = context;
    }
}