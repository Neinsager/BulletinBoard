using BulletinBoard.Domain.Base;
using BulletinBoard.Domain.Posts;
using BulletinBoard.Domain.Users;

namespace BulletinBoard.Domain.Comments
{
    /// <summary>
    /// Сущность комментария.
    /// </summary>
    public class Comment : BaseEntity
    {
        /// <summary>
        /// Содержимое комментария.
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// Объявление у которого оставили комментарий.
        /// </summary>
        public virtual Post Post { get; set; }

        /// <summary>
        /// Идентификатор объявления.
        /// </summary>
        public Guid PostId { get; set; }

        /// <summary>
        /// Пользователь.
        /// </summary>
        public virtual User User { get; set; }

        /// <summary>
        /// Идентификатор пользователя.
        /// </summary>
        public Guid UserId { get; set; }
    }
}
