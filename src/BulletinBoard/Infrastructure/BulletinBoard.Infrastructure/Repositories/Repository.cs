﻿using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BulletinBoard.Infrastructure.Repository
{
    /// <summary>
    /// Базовый репозиторий.
    /// </summary>
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected DbContext DbContext { get; }
        protected DbSet<TEntity> DbSet { get; }

        public Repository(DbContext context)
        {
            DbContext = context;
            DbSet = DbContext.Set<TEntity>();
        }

        /// <inheritdoc />
        public IQueryable<TEntity> GetAll()
        {
            return DbSet;
        }

        /// <inheritdoc />
        public IQueryable<TEntity> GetAllFiltered(Expression<Func<TEntity, bool>> predicat)
        {
            if (predicat == null)
            {
                throw new ArgumentNullException(nameof(predicat));
            }
            return DbSet.Where(predicat);
        }

        /// <inheritdoc />
        public async Task<TEntity> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return await DbSet.FindAsync(id, cancellationToken);
        }

        /// <inheritdoc />
        public async Task AddAsync(TEntity model , CancellationToken cancellationToken)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            await DbSet.AddAsync(model, cancellationToken);
            await SaveChangesAsync();
        }

        /// <inheritdoc />
        public async Task UpdateAsync(TEntity model, CancellationToken cancellationToken)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            DbSet.Update(model);
            await SaveChangesAsync();
        }

        /// <inheritdoc />
        public async Task DeleteAsync(TEntity model, CancellationToken cancellationToken)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            DbSet.Remove(model);
            await SaveChangesAsync();
        }

        /// <inheritdoc />
        public async Task SaveChangesAsync()
        {
            await DbContext.SaveChangesAsync();
        }
    }
}