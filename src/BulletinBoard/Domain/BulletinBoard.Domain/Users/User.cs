using BulletinBoard.Domain.Base;
using BulletinBoard.Domain.Comments;
using BulletinBoard.Domain.Posts;

namespace BulletinBoard.Domain.Users
{
    /// <summary>
    /// Класс Пользователя.
    /// </summary>
    public class User : BaseEntity
    {
        /// <summary>
        /// Имя.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Логин.
        /// </summary>
        public string Login { get; set; }

        /// <summary>
        /// Почта пользователя.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Хэш пароля.
        /// </summary>
        public string PasswordHash { get; set; }

        /// <summary>
        /// Роль пользователя.
        /// </summary>
        public string Role { get; set; }

        /// <summary>
        /// Номер телефона пользователя.
        /// </summary>
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Список объявлений.
        /// </summary>
        public virtual IEnumerable<Post>? Posts { get; set; }

        /// <summary>
        /// Комментарии.
        /// </summary>
        public virtual IEnumerable<Comment>? Comments { get; set; }
    }
}
