using BulletinBoard.Contracts.Base;
using BulletinBoard.Contracts.Comments;
using BulletinBoard.Contracts.Posts;

namespace BulletinBoard.Contracts.Users
{
    /// <summary>
    /// Пользователь.
    /// </summary>
    public class UserDto : BaseDto
    {
        /// <summary>
        /// Логин пользователя.
        /// </summary>
        public string Login { get; set; }

        /// <summary>
        /// Имя пользователя.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Роль пользователя.
        /// </summary>
        public string Role { get; set; }

        /// <summary>
        /// Объявления пользователя.
        /// </summary>     
        public IEnumerable<PostInfoDto>? Posts { get; set; }

        /// <summary>
        /// Комментарии пользователя.
        /// </summary>
        public IEnumerable<CommentInfoDto>? Comments { get; set; }
    }
}