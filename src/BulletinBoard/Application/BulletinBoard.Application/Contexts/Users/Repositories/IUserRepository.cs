using BulletinBoard.Contracts.Users;
using BulletinBoard.Domain.Users;
using System.Linq.Expressions;

namespace BulletinBoard.Application.AppServices.Contexts.Users.Repositories
{
    /// <summary>
    /// Репозиторий для работы с пользователями.
    /// </summary>
    public interface IUserRepository
    {
        /// <summary>
        /// Возвращает пользователя по заданному идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор пользователя.</param>
        /// <param name="cancellationToken">Токен отмены операции.</param>
        /// <returns>Модель пользователя <see cref="UserDto"/>.</returns>
        Task<UserDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken);

        /// <summary>
        /// Возвращает пользователя по заданному условию.
        /// </summary>
        /// <param name="predicate">Предиката условие для фильтра.</param>
        /// <param name="cancellationToken">Токен отмены операции.</param>
        /// <returns>Модель пользователя <see cref="User"/>.</returns>
        Task<User?> GetByPredicate(Expression<Func<User, bool>> predicate, CancellationToken cancellationToken);

        /// <summary>
        /// Возвращает текущего пользователя.
        /// </summary>
        /// <param name="id">Идентификатор.</param>
        /// <param name="cancellationToken">Токен отмены операции.</param>
        /// <returns>Модель пользователя <see cref="UserInfoDto"/></returns>
        Task<UserInfoDto> GetCurrentUser(Guid id, CancellationToken cancellationToken);

        /// <summary>
        /// Создает пользователя.
        /// </summary>
        /// <param name="user">Пользователь.</param>
        /// <param name="cancellationToken">Токен отмены операции.</param>
        /// <returns>Идентификатор созданной сущности.</returns>
        Task<Guid> CreateAsync(User user, CancellationToken cancellationToken);

        /// <summary>
        /// Редактирует пользователя.
        /// </summary>
        /// <param name="id">Идентификатор пользователя.</param>
        /// <param name="user">Пользователь.</param>
        /// <param name="cancellationToken">Токен отмены операции.</param>
        Task UpdateAsync(Guid id, User user, CancellationToken cancellationToken);

        /// <summary>
        /// Удаляет пользователя по идентификатору.
        /// </summary>
        /// <param name="user">Пользователь.</param>
        /// <param name="cancellationToken">Токен отмены операции.</param>
        Task DeleteAsync(User user, CancellationToken cancellationToken);

        /// <summary>
        /// Получить всех пользователей.
        /// </summary>
        /// <param name="cancellationToken">Токен отмены операции.</param>
        /// <returns>Список пользователей с краткой информацией.</returns>
        Task<List<UserInfoDto>> GetAllAsync(CancellationToken cancellationToken);
    }
}