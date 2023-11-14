using BulletinBoard.Contracts.Users;

namespace BulletinBoard.Application.AppServices.Contexts.Users.Services
{
    public interface IUserService
    {
        /// <summary>
        /// Возвращает пользователя по заданному идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор пользователя.</param>
        /// <param name="cancellationToken">Отмена операции.</param>
        /// <returns>Модель пользователя <see cref="UserDto"/>.</returns>
        Task<UserDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken);

        /// <summary>
        /// Возвращает текущего пользователя.
        /// </summary>
        /// <param name="cancellationToken">Отмена операции.</param>
        /// <returns>Модель пользователя <see cref="UserInfoDto"/></returns>
        Task<UserInfoDto> GetCurrentUser(CancellationToken cancellationToken);

        /// <summary>
        /// Редактирует данные пользователя.
        /// </summary>
        /// <param name="id">Идентификатор пользователя.</param>
        /// <param name="dto">Модель пользователя.</param>
        /// <param name="cancellationToken">Отмена операции.</param>
        Task UpdateAsync(Guid id, UpdateUserDto dto, CancellationToken cancellationToken);

        /// <summary>
        /// Удаляет пользователя по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор пользователя.</param>
        /// <param name="cancellationToken">Отмена операции.</param>
        Task DeleteAsync(Guid id, CancellationToken cancellationToken);

        /// <summary>
        /// Получить всех пользователей.
        /// </summary>
        /// <param name="cancellationToken">Токен отмены операции.</param>
        /// <returns>Список пользователей с краткой информацией.</returns>
        Task<List<UserInfoDto>> GetAllAsync(CancellationToken cancellationToken);
    }
}