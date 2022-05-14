using ToDo.Application.Interfaces;
using ToDo.Domain.Entities;
using ToDo.Domain.Enums;

namespace ToDo.Persistence.Seeds;

public static class DefaultPopulation
{
    public static async void AddDefaultPopulation(this IToDoUnitOfWork toDoUnitOfWork)
    {
        try
        {
            if (toDoUnitOfWork.ToDoListRepository.GetAllQueryable().Any())
                return;
            
            await toDoUnitOfWork.BeginTransactionAsync();

            var toDoList = new ToDoList
            {
                Title = "To do list #1",
                Description = "To do list description #1",
                Items = new List<ToDoItem>()
                {
                    new()
                    {
                        Title = "To do #1",
                        Note = "To do note #1",
                        Scheduled = DateTime.UtcNow.AddDays(5),
                        Done = false,
                    },
                    new()
                    {
                        Title = "To do #2",
                        Note = "To do note #2",
                        Scheduled = DateTime.UtcNow.AddDays(2),
                        Done = false,
                    },
                    new()
                    {
                        Title = "To do #3",
                        Note = "To do note #3",
                        Scheduled = DateTime.UtcNow.Subtract(TimeSpan.FromDays(2)),
                        Done = true,
                    }
                }
            };
            
            toDoUnitOfWork.ToDoListRepository.Add(toDoList);
            toDoUnitOfWork.ToDoItemRepository.AddRange(toDoList.Items.ToList());

            await toDoUnitOfWork.SaveChangesAsync();

            await toDoUnitOfWork.CommitTransactionAsync();
        }
        catch (Exception e)
        {
            await toDoUnitOfWork.RollbackTransactionAsync();
        }
    }
}