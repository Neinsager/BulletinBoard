using BulletinBoard.Contracts.Categories;
using BulletinBoard.Domain.Categories;
using System.Linq.Expressions;

namespace BulletinBoard.Application.AppServices.Contexts.Categories.Repositories
{
    /// <summary>
    /// Репозиторий для работы с категориями.
    /// </summary>
    public interface ICategoryRepository
    {
        /// <summary>
        /// Возвращает категорию по заданному идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор категории.</param>
        /// <param name="cancellationToken">Токен отмены операции.</param>
        /// <returns>Модель категории <see cref="CategoryDto"/>.</returns>
        Task<CategoryDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken);

        /// <summary>
        /// Возвращает ограниченный список категорий.
        /// </summary>
        /// <param name="cancellationToken">Токен отмены операции.</param>
        /// <returns>Коллекция категорий <see cref="CategoryDto"/>.</returns>
        Task<IReadOnlyCollection<CategoryInfoDto>> GetAllAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Возвращает категории по заданному условию.
        /// </summary>
        /// <param name="predicate">Предиката условие для фильтра.</param>
        /// <param name="cancellationToken">Токен отмены операции.</param>
        /// <returns>Сущность категории <see cref="Category"/>.</returns>
        Task<Category> GetByPredicate(Expression<Func<Category, bool>> predicate, CancellationToken cancellationToken);

        /// <summary>
        /// Создает категорию.
        /// </summary>
        /// <param name="category">Категория.</param>
        /// <param name="cancellationToken">Токен отмены операции.</param>
        /// <returns>Идентификатор созданной сущности.</returns>
        Task<Guid> CreateAsync(Category category, CancellationToken cancellationToken);

        /// <summary>
        /// Редактирует категорию.
        /// </summary>
        /// <param name="id">Идентификатор категории.</param>
        /// <param name="category">Категория.</param>
        /// <param name="cancellationToken">Токен отмены операции.</param>
        Task UpdateAsync(Guid id, Category category, CancellationToken cancellationToken);

        /// <summary>
        /// Удалить категорию по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор категории.</param>
        /// <param name="cancellationToken">Токен отмены операции.</param>
        Task DeleteByIdAsync(Guid id, CancellationToken cancellationToken);
    }
}
