using BulletinBoard.Contracts.Base;

namespace BulletinBoard.Contracts.Users
{
    /// <summary>
    /// Информация о пользователе.
    /// </summary>

    public class UserInfoDto : BaseDto
    {
        /// <summary>
        /// Имя пользователя.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Номер телефона
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// Роль пользователя.
        /// </summary>
        public string Role { get; set; }
    }
}

