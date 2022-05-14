using System.Data.Common;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Storage;
using ToDo.Application.Interfaces;

namespace ToDo.Persistence.Base;

public abstract class BaseUnitOfWork<T> where T : IDbContext
{
    private readonly T _dbContext;

    public BaseUnitOfWork(T context)
    {
        _dbContext = context;
    }

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) =>
        await _dbContext.SaveChangesAsync(cancellationToken);

    public async Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default) =>
        await _dbContext.BeginTransactionAsync(cancellationToken);

    public async Task CommitTransactionAsync(CancellationToken cancellationToken = default) =>
        await _dbContext.CommitTransactionAsync(cancellationToken);

    public async Task RollbackTransactionAsync(CancellationToken cancellationToken = default) =>
        await _dbContext.RollbackTransactionAsync(cancellationToken);

    public DbConnection GetConnection() => _dbContext.GetConnection();


    /// <summary>
    /// <see cref="TContext.SetAsAdded{TEntity}(TEntity, CancellationToken)"></see>
    /// </summary>
    public void SetAsAdded<TEntity>(TEntity entity, CancellationToken cancellationToken = default) where TEntity : class
    {
        _dbContext.SetAsAdded<TEntity>(entity, cancellationToken);
    }

    /// <summary>
    /// <see cref="TContext.SetAsAdded{TEntity}(List{TEntity}, CancellationToken)"></see>
    /// </summary>
    public void SetAsAdded<TEntity>(List<TEntity> entities, CancellationToken cancellationToken = default)
        where TEntity : class
    {
        _dbContext.SetAsAdded<TEntity>(entities, cancellationToken);
    }

    /// <summary>
    /// <see cref="TContext.SetAsModified{TEntity}(TEntity, CancellationToken)"></see>
    /// </summary>
    public void SetAsModified<TEntity>(TEntity entity, CancellationToken cancellationToken = default)
        where TEntity : class
    {
        _dbContext.SetAsModified<TEntity>(entity, cancellationToken);
    }

    /// <summary>
    /// <see cref="TContext.SetAsModified{TEntity}(List{TEntity}, CancellationToken)"></see>
    /// </summary>
    public void SetAsModified<TEntity>(List<TEntity> entities, CancellationToken cancellationToken = default)
        where TEntity : class
    {
        _dbContext.SetAsModified<TEntity>(entities, cancellationToken);
    }

    /// <summary>
    /// <see cref="TContext.SetAsDeleted{TEntity}(TEntity, CancellationToken)"></see>
    /// </summary>
    public void SetAsDeleted<TEntity>(TEntity entity, CancellationToken cancellationToken = default)
        where TEntity : class
    {
        _dbContext.SetAsDeleted<TEntity>(entity, cancellationToken);
    }

    /// <summary>
    /// <see cref="TContext.SetAsDeleted{TEntity}(List{TEntity}, CancellationToken)"></see>
    /// </summary>
    public void SetAsDeleted<TEntity>(List<TEntity> entities, CancellationToken cancellationToken = default)
        where TEntity : class
    {
        _dbContext.SetAsDeleted<TEntity>(entities, cancellationToken);
    }


    /// <summary>
    /// <see cref="TContext.FindAsync{TEntity}(Guid, CancellationToken)"></see>
    /// </summary>
    public async Task<TEntity> FindAsync<TEntity>(Guid id, CancellationToken cancellationToken = default)
        where TEntity : class
    {
        return await _dbContext.FindAsync<TEntity>(id, cancellationToken);
    }

    /// <summary>
    /// <see cref="TContext.FindByCriteriaAsync{TEntity}(Expression, CancellationToken)"></see>
    /// </summary>
    public async Task<TEntity> FindByCriteriaAsync<TEntity>(Expression<Func<TEntity, bool>> predicate,
        CancellationToken cancellationToken = default) where TEntity : class
    {
        return await _dbContext.FindByCriteriaAsync<TEntity>(predicate, cancellationToken);
    }

    /// <summary>
    /// <see cref="TContext.FirstOrDefaultAsync{TEntity}(Expression{Func{TEntity, bool}}, CancellationToken)"></see>
    /// </summary>
    public async Task<TEntity> FirstOrDefaultAsync<TEntity>(Expression<Func<TEntity, bool>> predicate,
        CancellationToken cancellationToken = default) where TEntity : class
    {
        return await _dbContext.FirstOrDefaultAsync<TEntity>(predicate, cancellationToken);
    }

    /// <summary>
    /// <see cref="TContext.ToListAsync{TEntity}(CancellationToken)"></see>
    /// </summary>
    public async Task<List<TEntity>> ToListAsync<TEntity>(CancellationToken cancellationToken = default)
        where TEntity : class
    {
        return await _dbContext.ToListAsync<TEntity>(cancellationToken);
    }

    /// <summary>
    /// <see cref="TContext.ToListByCriteriaAsync{TEntity}(Expression{Func{TEntity, bool}}, CancellationToken)"></see>
    /// </summary>
    public async Task<List<TEntity>> ToListByCriteriaAsync<TEntity>(Expression<Func<TEntity, bool>> predicate,
        CancellationToken cancellationToken = default) where TEntity : class
    {
        return await _dbContext.ToListByCriteriaAsync<TEntity>(predicate, cancellationToken);
    }

    /// <summary>
    /// <see cref="TContext.AnyAsync{TEntity}(Expression{Func{TEntity, bool}}, CancellationToken)"></see>
    /// </summary>
    public async Task<bool> AnyAsync<TEntity>(Expression<Func<TEntity, bool>> predicate,
        CancellationToken cancellationToken = default) where TEntity : class
    {
        return await _dbContext.AnyAsync<TEntity>(predicate, cancellationToken);
    }

    /// <summary>
    /// <see cref="TContext.ToQueryable{TEntity}(CancellationToken)"></see>
    /// </summary>
    public IQueryable<TEntity> ToQueryable<TEntity>(CancellationToken cancellationToken = default) where TEntity : class
    {
        return _dbContext.ToQueryable<TEntity>(cancellationToken);
    }

    /// <summary>
    /// <see cref="TContext.ToQueryableByCriteria{TEntity}(Expression{Func{TEntity, bool}}, CancellationToken)"></see>
    /// </summary>
    public IQueryable<TEntity> ToQueryableByCriteria<TEntity>(Expression<Func<TEntity, bool>> predicate,
        CancellationToken cancellationToken = default) where TEntity : class
    {
        return _dbContext.ToQueryableByCriteria<TEntity>(predicate, cancellationToken);
    }
}