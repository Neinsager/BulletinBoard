using System.ComponentModel.DataAnnotations;

namespace BulletinBoard.Contracts.Users
{
    /// <summary>
    /// Модель для аутентификации пользователя.
    /// </summary>
    public class AuthUserDto
    {
        /// <summary>
        /// Логин.
        /// </summary>
        [Required]
        [StringLength(16, MinimumLength = 4)]
        public string Login { get; init; }

        /// <summary>
        /// Пароль.
        /// </summary>
        [Required]
        [StringLength(16, MinimumLength = 8)]
        public string Password { get; init; }
    }
}