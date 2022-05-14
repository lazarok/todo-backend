using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using ToDo.Application.Interfaces;
using ToDo.Application.Interfaces.Repositories.Common;

namespace ToDo.Persistence.Base;

/// <summary>
/// This class implement <see cref="IRepository{TEntity}<TEntity>"/>
/// </summary>
/// <typeparam name="TEntity"></typeparam>
public class BaseRepository<TEntity> : IRepository<TEntity> where TEntity : class
{
    /// <summary>
    /// The DataBase Context
    /// </summary>
    private readonly IDbContext _dbContext;

    /// <summary>
    /// Initializes a new instance of the <see cref="Repository{TEntity}" /> class
    /// </summary>
    /// <param name="context">The implementation of Database Context <see cref="IDbContext" /></param>
    public BaseRepository(IDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    /// <summary>
    /// <see cref="IRepository{TEntity}.GetAllAsync(CancellationToken)"/>
    /// </summary>
    public Task<List<TEntity>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        return _dbContext.ToListAsync<TEntity>(cancellationToken);
    }

    /// <summary>
    /// <see cref="IRepository{TEntity}.GetAllQueryable(CancellationToken)"/>
    /// </summary>
    public IQueryable<TEntity> GetAllQueryable(CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        return _dbContext.ToQueryable<TEntity>(cancellationToken);
    }

    /// <summary>
    /// <see cref="IRepository{TEntity}.GetAllByCriteriaAsync(Expression, CancellationToken)"/>
    /// </summary>
    public Task<List<TEntity>> GetAllByCriteriaAsync(Expression<Func<TEntity, bool>> predicate,
        CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        return _dbContext.ToListByCriteriaAsync<TEntity>(predicate, cancellationToken);
    }

    /// <summary>
    /// <see cref="IRepository{TEntity}.AnyAsync(Expression{Func{TEntity, bool}}, CancellationToken)"/>
    /// </summary>
    public Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        return _dbContext.AnyAsync<TEntity>(predicate, cancellationToken);
    }

    /// <summary>
    /// <see cref="IRepository{TEntity}.GetAllQueryableByCriteria(Expression{Func{TEntity, bool}}, CancellationToken)"/>
    /// </summary>
    public IQueryable<TEntity> GetAllQueryableByCriteria(Expression<Func<TEntity, bool>> predicate,
        CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        return _dbContext.ToQueryableByCriteria<TEntity>(predicate, cancellationToken);
    }

    /// <summary>
    /// <see cref="IRepository{TEntity}.GetByCriteriaAsync(Expression{Func{TEntity, bool}}, CancellationToken)"/>
    /// </summary>
    public Task<TEntity> GetByCriteriaAsync(Expression<Func<TEntity, bool>> predicate,
        CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        return _dbContext.FirstOrDefaultAsync<TEntity>(predicate, cancellationToken);
    }

    /// <summary>
    /// <see cref="IRepository{TEntity}.FindByCriteriaAsync(Expression{Func{TEntity, bool}}, CancellationToken)"/>
    /// </summary>
    public Task<TEntity> FindByCriteriaAsync(Expression<Func<TEntity, bool>> predicate,
        CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        return _dbContext.FindByCriteriaAsync<TEntity>(predicate, cancellationToken);
    }

    /// <summary>
    /// <see cref="IRepository{TEntity}.FindAsync(Guid, CancellationToken)"/>
    /// </summary>
    public Task<TEntity> FindAsync(Guid id, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        return _dbContext.FindAsync<TEntity>(id, cancellationToken);
    }

    /// <summary>
    /// <see cref="IRepository{TEntity}.Add(TEntity, CancellationToken)/>
    /// </summary>
    public void Add(TEntity entity, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        _dbContext.SetAsAdded<TEntity>(entity, cancellationToken);
    }

    /// <summary>
    /// <see cref="IRepository{TEntity}.AddRange(List{TEntity}, CancellationToken)"/
    /// </summary>
    public void AddRange(List<TEntity> entities, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        _dbContext.SetAsAdded<TEntity>(entities, cancellationToken);
    }

    /// <summary>
    /// <see cref="DbLoggerCategory.Update"/>
    /// </summary>
    public void Update(TEntity entity, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        _dbContext.SetAsModified<TEntity>(entity, cancellationToken);
    }

    /// <summary>
    /// <see cref="IRepository{TEntity}.UpdateRange(List{TEntity}, CancellationToken)"/>
    /// </summary>
    public void UpdateRange(List<TEntity> entities, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        _dbContext.SetAsModified<TEntity>(entities, cancellationToken);
    }

    /// <summary>
    /// <see cref="IRepository{TEntity}.Delete(TEntity, CancellationToken)"/>
    /// </summary>
    public void Delete(TEntity entity, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        _dbContext.SetAsDeleted<TEntity>(entity, cancellationToken);
    }

    /// <summary>
    ///  <see cref="IRepository{TEntity}.DeleteRange(List{TEntity}, CancellationToken)"/>
    /// </summary>
    public void DeleteRange(List<TEntity> entities, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        _dbContext.SetAsDeleted<TEntity>(entities, cancellationToken);
    }
}