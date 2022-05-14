using System.Data.Common;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Storage;

namespace ToDo.Application.Interfaces;

public interface IDbContext
{
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default);
    Task CommitTransactionAsync(CancellationToken cancellationToken = default);
    Task RollbackTransactionAsync(CancellationToken cancellationToken = default);

    DbConnection GetConnection();

    /// <summary>
    /// Add an Entity
    /// </summary>
    /// <param name="entity">The Entity</param>
    /// <param name="cancellationToken">Notification that operations should be canceled</param>
    void SetAsAdded<TEntity>(TEntity entity, CancellationToken cancellationToken = default) where TEntity : class;

    /// <summary>
    /// Add an List of Entities
    /// </summary>
    /// <param name="entities">The List of Entities</param>
    /// <param name="cancellationToken">Notification that operations should be canceled</param>
    void SetAsAdded<TEntity>(List<TEntity> entities, CancellationToken cancellationToken = default)
        where TEntity : class;

    /// <summary>
    /// Update an Entity
    /// </summary>
    /// <param name="entity">The Entity</param>
    /// <param name="cancellationToken">Notification that operations should be canceled</param>
    void SetAsModified<TEntity>(TEntity entity, CancellationToken cancellationToken = default) where TEntity : class;

    /// <summary>
    /// Update an List of Entities
    /// </summary>
    /// <param name="entities">The List of Entities</param>
    /// <param name="cancellationToken">Notification that operations should be canceled</param>
    void SetAsModified<TEntity>(List<TEntity> entities, CancellationToken cancellationToken = default)
        where TEntity : class;

    /// <summary>
    /// Delete an Entity
    /// </summary>
    /// <param name="entity">The Entity</param>
    /// <param name="cancellationToken">Notification that operations should be canceled</param>
    void SetAsDeleted<TEntity>(TEntity entity, CancellationToken cancellationToken = default) where TEntity : class;

    /// <summary>
    /// Delete an List of Entities
    /// </summary>
    /// <param name="entities">The List of Entities</param>
    /// <param name="cancellationToken">Notification that operations should be canceled</param>
    void SetAsDeleted<TEntity>(List<TEntity> entities, CancellationToken cancellationToken = default)
        where TEntity : class;

    /// <summary>
    /// Obtain a list of async generic Entities
    /// </summary>
    /// <param name="cancellationToken">Notification that operations should be canceled</param>
    /// <returns>A list of async generic Entities</returns>
    Task<List<TEntity>> ToListAsync<TEntity>(CancellationToken cancellationToken = default) where TEntity : class;

    /// <summary>
    /// Obtain a list of async generic Entities by criteria
    /// </summary>
    /// <param name="predicate">The criteria</param>
    /// <param name="cancellationToken">Notification that operations should be canceled</param>
    /// <returns>A list of async generic Entities by criteria</returns>
    Task<List<TEntity>> ToListByCriteriaAsync<TEntity>(Expression<Func<TEntity, bool>> predicate,
        CancellationToken cancellationToken = default) where TEntity : class;

    /// <summary>
    /// Obtain if exist some data on BD using that predicate
    /// </summary>
    /// <param name="predicate">The criteria</param>
    /// <param name="cancellationToken">Notification that operations should be canceled</param>
    /// <returns>If exist some data on BD using that predicate</returns>
    Task<bool> AnyAsync<TEntity>(Expression<Func<TEntity, bool>> predicate,
        CancellationToken cancellationToken = default) where TEntity : class;

    /// <summary>
    /// Obtain a list query of async generic Entities
    /// </summary>
    /// <param name="cancellationToken">Notification that operations should be canceled</param>
    /// <returns>A list query of async generic Entities</returns>
    IQueryable<TEntity> ToQueryable<TEntity>(CancellationToken cancellationToken = default) where TEntity : class;

    /// <summary>
    /// Obtain a list query of async generic Entities by criteria
    /// </summary>
    /// <param name="predicate">The criteria</param>
    /// <param name="cancellationToken">Notification that operations should be canceled</param>
    /// <returns>A list query of async generic Entities</returns>
    IQueryable<TEntity> ToQueryableByCriteria<TEntity>(Expression<Func<TEntity, bool>> predicate,
        CancellationToken cancellationToken = default) where TEntity : class;

    /// <summary>
    /// Obtain first or something async generic Entity
    /// </summary>
    /// <param name="predicate">The criteria</param>
    /// <param name="cancellationToken">Notification that operations should be canceled</param>
    /// <returns>An Entity or default</returns>
    Task<TEntity> FirstOrDefaultAsync<TEntity>(Expression<Func<TEntity, bool>> predicate,
        CancellationToken cancellationToken = default) where TEntity : class;

    /// <summary>
    /// Obtain the entity on memory by Id
    /// </summary>
    /// <param name="id">The id</param>
    /// <param name="cancellationToken">Notification that operations should be canceled</param>
    /// <returns>An Entity</returns>
    Task<TEntity> FindAsync<TEntity>(Guid id, CancellationToken cancellationToken = default) where TEntity : class;

    /// <summary>
    /// Obtain the entity on memory by criteria
    /// </summary>
    /// <param name="predicate">The criteria</param>
    /// <param name="cancellationToken">Notification that operations should be canceled</param>
    /// <returns>An Entity</returns>
    Task<TEntity> FindByCriteriaAsync<TEntity>(Expression<Func<TEntity, bool>> predicate,
        CancellationToken cancellationToken = default) where TEntity : class;
}