using BulletinBoard.Contracts.Base;
using BulletinBoard.Contracts.Posts;
using BulletinBoard.Contracts.Users;

namespace BulletinBoard.Contracts.Comments
{
    /// <summary>
    /// Комментарий.
    /// </summary>
    public class CommentDto: BaseDto
    {
        /// <summary>
        /// Содержимое комментария.
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// Объявление которому принадлежит комментарий.
        /// </summary>
        public PostInfoDto PostDto { get; set; }

        /// <summary>
        /// Идентификатор объявления которому принадлежит комментарий. 
        /// </summary>
        public Guid PostId { get; set; }

        /// <summary>
        /// Пользователь оставивший комментарий.
        /// </summary>
        public UserInfoDto User { get; set; }

        /// <summary>
        /// Идентификатор пользователя оставивший комментарий.
        /// </summary>
        public Guid UserId { get; set; }
    }
}
