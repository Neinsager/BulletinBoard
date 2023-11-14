using System.Linq.Expressions;

namespace BulletinBoard.Infrastructure.Repository
{
    /// <summary>
    /// Базовый репозиторий.
    /// </summary>
    /// <typeparam name="TEntity">Тип сущности.</typeparam>
    public interface IRepository<TEntity> where TEntity : class
    {
        /// <summary>
        /// Получить все элементы сущности <see cref="TEntity" />.
        /// </summary>
        /// <returns>Все элементы сущности <see cref="TEntity" /></returns>
        IQueryable<TEntity> GetAll();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="predicat"></param>
        /// <returns></returns>
        IQueryable<TEntity> GetAllFiltered(Expression<Func<TEntity, bool>> predicat);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<TEntity> GetByIdAsync(Guid id, CancellationToken cancellationToken);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task AddAsync(TEntity model, CancellationToken cancellationToken);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task UpdateAsync(TEntity model, CancellationToken cancellationToken);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task DeleteAsync(TEntity model , CancellationToken cancellationToken);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        Task SaveChangesAsync();
    }
}