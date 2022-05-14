using Microsoft.EntityFrameworkCore;
using ToDo.Application.Interfaces;
using ToDo.Application.Repositories;
using ToDo.Persistence.Base;
using ToDo.Persistence.Repositories;

namespace ToDo.Persistence;

public class ToDoUnitOfWorkContainer: BaseUnitOfWork<IToDoDbContext>, IToDoUnitOfWork
    {
        public IToDoItemRepository ToDoItemRepository { get; }
        public IToDoListRepository ToDoListRepository { get; }

        public ToDoUnitOfWorkContainer(IToDoDbContext context) : base(context)
        {
            ToDoItemRepository = new ToDoItemRepository(context);
            ToDoListRepository = new ToDoListRepository(context);
        }
    }