using BulletinBoard.Contracts.Attachments;
using BulletinBoard.Contracts.Base;
using BulletinBoard.Contracts.Categories;
using BulletinBoard.Contracts.Comments;
using BulletinBoard.Contracts.Users;

namespace BulletinBoard.Contracts.Posts
{
    /// <summary>
    /// Обьявление.
    /// </summary>
    public class PostDto : BaseDto
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
        /// Цена.
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Категория.
        /// </summary>
        public CategoryDto Category { get; set; }

        /// <summary>
        /// Изображения.
        /// </summary>
        public IReadOnlyCollection<AttachmentDto> Attachments { get; set; }

        /// <summary>
        /// Комментарии.
        /// </summary>
        public IReadOnlyCollection<CommentDto> Comments { get; set; }

        /// <summary>
        /// Пользователь.
        /// </summary>
        public UserDto User { get; set; }

        /// <summary>
        /// Пользователь.
        /// </summary>
        public Guid UserId { get; set; }
    }
}
