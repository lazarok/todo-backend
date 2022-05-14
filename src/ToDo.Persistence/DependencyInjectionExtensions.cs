using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ToDo.Application.Interfaces;
using ToDo.Persistence.Seeds;

namespace ToDo.Persistence;

public static class DependencyInjectionExtensions
{
    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ToDoDbContext>(options =>
            options.UseSqlServer(
                configuration.GetConnectionString("ToDoConnection"),
                b => b.MigrationsAssembly(typeof(ToDoDbContext).Assembly.FullName)));

        services.AddScoped<IToDoDbContext>(sp => sp.GetService<ToDoDbContext>());
        services.AddScoped<IToDoUnitOfWork, ToDoUnitOfWorkContainer>();

        #region Seeds

        var container = services.BuildServiceProvider();
        var toDoUnitOfWork = container.GetRequiredService<IToDoUnitOfWork>();

        toDoUnitOfWork.AddDefaultPopulation();

        #endregion

        return services;
    }
}