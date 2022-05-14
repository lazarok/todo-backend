using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using ToDo.Application.Interfaces;
using ToDo.Application.Interfaces.Services;
using ToDo.Domain.Common;
using ToDo.Domain.Entities;
using ToDo.Persistence.Base;

namespace ToDo.Persistence;

public class ToDoDbContext: BaseContext<ToDoDbContext>, IToDoDbContext
{
    private readonly ICurrentUserService _currentUserService;
    private readonly IDateTimeService _dateTimeService;
    
    public DbSet<ToDoItem> ToDoItems { get; set; }
    public DbSet<ToDoList> ToDoLists { get; set; }
    
    public ToDoDbContext(DbContextOptions<ToDoDbContext> options, ICurrentUserService currentUserService, IDateTimeService dateTimeService)
        : base(options)
    {
        _currentUserService = currentUserService;
        _dateTimeService = dateTimeService;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(modelBuilder);
    }
    
    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        OnBeforeSaving();
        return base.SaveChangesAsync(cancellationToken);
    }

    public Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default)
    {
        return Database.BeginTransactionAsync(cancellationToken);
    }

    public Task CommitTransactionAsync(CancellationToken cancellationToken = default)
    {
        return Database.CommitTransactionAsync(cancellationToken);
    }

    public Task RollbackTransactionAsync(CancellationToken cancellationToken = default)
    {
        return Database.RollbackTransactionAsync(cancellationToken);
    }

    private void OnBeforeSaving()
    {
        var dateTimeUtcNow = _dateTimeService.UtcNow;

        foreach (var entry in ChangeTracker.Entries<AuditableBaseEntity>())
        {
            if (entry.State == EntityState.Added)
            {
                entry.Entity.CreatedBy = _currentUserService.UserId?.ToString();
                entry.Entity.CreatedAt = dateTimeUtcNow;
            }
            else if (entry.State == EntityState.Modified)
            {
                entry.Entity.LastModifiedBy = _currentUserService.UserId?.ToString();
                entry.Entity.LastModifiedAt = dateTimeUtcNow;
            }
        }
    }
}