using System.Data.Common;
using System.Linq.Expressions;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage;

namespace ToDo.Persistence.Base;

 public abstract class BaseContext<TContext> : DbContext where TContext : DbContext
    {
        protected BaseContext(DbContextOptions<TContext> options)
            : base(options)
        {
        }
        
        public async Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default) =>
            await Database.BeginTransactionAsync(cancellationToken);

        public async Task CommitTransactionAsync(CancellationToken cancellationToken = default) =>
            await Database.CommitTransactionAsync(cancellationToken);

        public async Task RollbackTransactionAsync(CancellationToken cancellationToken = default) =>
            await Database.RollbackTransactionAsync(cancellationToken);

        public DbConnection GetConnection() => default; //Database.

        /// <summary>
        /// <see cref="TContext.SetAsAdded{TEntity}(TEntity, CancellationToken)"></see>
        /// </summary>
        public void SetAsAdded<TEntity>(TEntity entity, CancellationToken cancellationToken = default) where TEntity : class
        {
            cancellationToken.ThrowIfCancellationRequested();

            this.UpdateEntityState<TEntity>(entity, EntityState.Added, cancellationToken);
        }

        /// <summary>
        /// <see cref="TContext.SetAsAdded{TEntity}(List{TEntity}, CancellationToken)"></see>
        /// </summary>
        public void SetAsAdded<TEntity>(List<TEntity> entities, CancellationToken cancellationToken = default) where TEntity : class
        {
            cancellationToken.ThrowIfCancellationRequested();

            entities.ForEach(e => this.SetAsAdded<TEntity>(e, cancellationToken));
        }

        /// <summary>
        /// <see cref="TContext.SetAsModified{TEntity}(TEntity, CancellationToken)"></see>
        /// </summary>
        public void SetAsModified<TEntity>(TEntity entity, CancellationToken cancellationToken = default) where TEntity : class
        {
            cancellationToken.ThrowIfCancellationRequested();

            this.UpdateEntityState<TEntity>(entity, EntityState.Modified, cancellationToken);
        }

        /// <summary>
        /// <see cref="TContext.SetAsModified{TEntity}(List{TEntity}, CancellationToken)"></see>
        /// </summary>
        public void SetAsModified<TEntity>(List<TEntity> entities, CancellationToken cancellationToken = default) where TEntity : class
        {
            cancellationToken.ThrowIfCancellationRequested();

            entities.ForEach(e => this.SetAsModified<TEntity>(e, cancellationToken));
        }

        /// <summary>
        /// <see cref="TContext.SetAsDeleted{TEntity}(TEntity, CancellationToken)"></see>
        /// </summary>
        public void SetAsDeleted<TEntity>(TEntity entity, CancellationToken cancellationToken = default) where TEntity : class
        {
            cancellationToken.ThrowIfCancellationRequested();

            this.UpdateEntityState<TEntity>(entity, EntityState.Deleted, cancellationToken);
        }

        /// <summary>
        /// <see cref="TContext.SetAsDeleted{TEntity}(List{TEntity}, CancellationToken)"></see>
        /// </summary>
        public void SetAsDeleted<TEntity>(List<TEntity> entities, CancellationToken cancellationToken = default) where TEntity : class
        {
            cancellationToken.ThrowIfCancellationRequested();

            entities.ForEach(e => this.SetAsDeleted<TEntity>(e, cancellationToken));
        }


        /// <summary>
        /// <see cref="TContext.FindAsync{TEntity}(Guid, CancellationToken)"></see>
        /// </summary>
        public Task<TEntity> FindAsync<TEntity>(Guid id, CancellationToken cancellationToken = default) where TEntity : class
        {
            cancellationToken.ThrowIfCancellationRequested();

            return this.Set<TEntity>().FindAsync(id).AsTask();
        }

        /// <summary>
        /// <see cref="TContext.FindByCriteriaAsync{TEntity}(Expression, CancellationToken)"></see>
        /// </summary>
        public Task<TEntity> FindByCriteriaAsync<TEntity>(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default) where TEntity : class
        {
            cancellationToken.ThrowIfCancellationRequested();

            return this.Set<TEntity>().Local.AsQueryable().FirstOrDefaultAsync(predicate, cancellationToken) ?? this.FirstOrDefaultAsync<TEntity>(predicate, cancellationToken);
        }

        /// <summary>
        /// <see cref="TContext.FirstOrDefaultAsync{TEntity}(Expression{Func{TEntity, bool}}, CancellationToken)"></see>
        /// </summary>
        public Task<TEntity> FirstOrDefaultAsync<TEntity>(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default) where TEntity : class
        {
            cancellationToken.ThrowIfCancellationRequested();

            return this.Set<TEntity>().FirstOrDefaultAsync(predicate, cancellationToken);
        }

        /// <summary>
        /// <see cref="TContext.ToListAsync{TEntity}(CancellationToken)"></see>
        /// </summary>
        public Task<List<TEntity>> ToListAsync<TEntity>(CancellationToken cancellationToken = default) where TEntity : class
        {
            cancellationToken.ThrowIfCancellationRequested();

            return this.Set<TEntity>().ToListAsync(cancellationToken);
        }

        /// <summary>
        /// <see cref="TContext.ToListByCriteriaAsync{TEntity}(Expression{Func{TEntity, bool}}, CancellationToken)"></see>
        /// </summary>
        public Task<List<TEntity>> ToListByCriteriaAsync<TEntity>(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default) where TEntity : class
        {
            cancellationToken.ThrowIfCancellationRequested();

            return this.Set<TEntity>().Where(predicate).ToListAsync<TEntity>(cancellationToken);
        }

        /// <summary>
        /// <see cref="TContext.AnyAsync{TEntity}(Expression{Func{TEntity, bool}}, CancellationToken)"></see>
        /// </summary>
        public Task<bool> AnyAsync<TEntity>(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default) where TEntity : class
        {
            cancellationToken.ThrowIfCancellationRequested();

            return this.Set<TEntity>().AnyAsync(predicate, cancellationToken);
        }

        /// <summary>
        /// <see cref="TContext.ToQueryable{TEntity}(CancellationToken)"></see>
        /// </summary>
        public IQueryable<TEntity> ToQueryable<TEntity>(CancellationToken cancellationToken = default) where TEntity : class
        {
            cancellationToken.ThrowIfCancellationRequested();

            return this.Set<TEntity>().AsQueryable();
        }

        /// <summary>
        /// <see cref="TContext.ToQueryableByCriteria{TEntity}(Expression{Func{TEntity, bool}}, CancellationToken)"></see>
        /// </summary>
        public IQueryable<TEntity> ToQueryableByCriteria<TEntity>(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default) where TEntity : class
        {
            cancellationToken.ThrowIfCancellationRequested();

            return this.Set<TEntity>().Where(predicate).AsQueryable();
        }

        /// <summary>
        /// Update entity state
        /// </summary>
        private void UpdateEntityState<TEntity>(TEntity entity, EntityState entityState, CancellationToken cancellationToken = default) where TEntity : class
        {
            cancellationToken.ThrowIfCancellationRequested();

            var entityEntry = this.GetDbEntityEntrySafely<TEntity>(entity, cancellationToken);
            if (entityEntry.State == EntityState.Unchanged)
            {
                entityEntry.State = entityState;
            }
            
        }

        /// <summary>
        /// Attach entity
        /// </summary>
        private EntityEntry GetDbEntityEntrySafely<TEntity>(TEntity entity, CancellationToken cancellationToken = default) where TEntity : class
        {
            cancellationToken.ThrowIfCancellationRequested();

            var entityEntry = Entry<TEntity>(entity);
            if (entityEntry.State == EntityState.Detached)
            {
                this.Set<TEntity>().Attach(entity);
            }

            return entityEntry;
        }

    }