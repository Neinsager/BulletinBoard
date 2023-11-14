using BulletinBoard.Contracts.Users;

namespace BulletinBoard.Application.AppServices.Authentication.Services
{
    /// <summary>
    /// Сервис аутентификации и регистрации пользователей.
    /// </summary>
    public interface IAuthenticationService
    {
        /// <summary>
        /// Регистрация пользователя и сохранение его в БД.
        /// </summary>
        /// <param name="dto">Модель создания пользователя.</param>
        /// <param name="cancellationToken">Токен отмены операции.</param>
        /// <returns>Уникальный идентификатор созданного пользователя.</returns>
        Task<Guid> Register(CreateUserDto dto, CancellationToken cancellationToken);

        /// <summary>
        /// Логин пользователя в систему.
        /// </summary>
        /// <param name="dto">Модель аутентицфикации пользователя.</param>
        /// <param name="cancellationToken">Токен отмены операции.</param>
        /// <returns>JWT.</returns>
        Task<string> Login(AuthUserDto dto, CancellationToken cancellationToken);
    }
}