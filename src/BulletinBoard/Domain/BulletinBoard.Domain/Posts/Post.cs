using BulletinBoard.Domain.Attachments;
using BulletinBoard.Domain.Base;
using BulletinBoard.Domain.Categories;
using BulletinBoard.Domain.Comments;
using BulletinBoard.Domain.Users;

namespace BulletinBoard.Domain.Posts
{
    /// <summary>
    /// Сущность обьявления.
    /// </summary>
    public class Post : BaseEntity
    {
        /// <summary>
        /// Заголовок.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Описание.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Категория.
        /// </summary>
        public virtual Category Category { get; set; }

        /// <summary>
        /// Идентификатор категории.
        /// </summary>
        public Guid CategoryId { get; set; }

        /// <summary>
        /// Цена.
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Вложения.
        /// </summary>
        public virtual ICollection<Attachment> Attachments { get; set; }

        /// <summary>
        /// Комментарии.
        /// </summary>
        public virtual ICollection<Comment> Comments { get; set; }

        /// <summary>
        /// Идентификатор пользователя.
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// Пользователь.
        /// </summary>
        public virtual User User { get; set; }
    }
}
