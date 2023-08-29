using BulletinBoard.Domain.Base;

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
        /// Цена.
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Наименование тегов.
        /// </summary>
        public string[] TagNames { get; set; }

        /// <summary>
        /// Наименование категории.
        /// </summary>
        public Guid CategoryId { get; set; }
    }
}
