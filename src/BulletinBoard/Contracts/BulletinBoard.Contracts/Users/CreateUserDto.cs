using System.ComponentModel.DataAnnotations;

namespace BulletinBoard.Contracts.Users
{
    /// <summary>
    /// Создание пользователя.
    /// </summary>
    public class CreateUserDto
    {
        /// <summary>
        /// Имя.
        /// </summary>
        [Required(ErrorMessage = "Поле {0} не может быть пустым.")]
        [StringLength(32, MinimumLength = 2, ErrorMessage = "Поле {0} должно содержать от {2} до {1} символов.")]
        public string Name { get; init; }

        /// <summary>
        /// Имя пользователя.
        /// </summary>
        [Required(ErrorMessage = "Поле {0} не может быть пустым.")]
        [StringLength(32, MinimumLength = 4, ErrorMessage = "Поле {0} должно содержать от {2} до {1} символов.")]
        public string Login { get; set; }

        /// <summary>
        /// Пароль пользователя.
        /// </summary>
        [Required(ErrorMessage = "Поле {0} не может быть пустым.")]
        [StringLength(16, MinimumLength = 8, ErrorMessage = "Поле {0} должно содержать от {2} до {1} символов.")]
        public string Password { get; set; }

        /// <summary>
        /// Подтверждение пароля пользователя.
        /// </summary>
        [Compare("Password", ErrorMessage = "Пароли не совпадают")]
        [StringLength(16, MinimumLength = 8, ErrorMessage = "Поле {0} должно содержать от {2} до {1} символов.")]
        public string PasswordConfirm { get; set; }

        /// <summary>
        /// Номер телефона
        /// </summary>
        [Required(ErrorMessage = "Поле {0} не может быть пустым.")]
        [Phone(ErrorMessage = "Неккоректно введен номер телефона.")]
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Адрес электронной почты
        /// </summary>
        [Required(ErrorMessage = "Поле {0} не может быть пустым.")]
        [EmailAddress(ErrorMessage = "Неккоректно введен адрес электронной почты.")]
        public string Email { get; set; }
    }
}
