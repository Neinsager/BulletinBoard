using BulletinBoard.Contracts.Base;

namespace BulletinBoard.Contracts.Post
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
        /// Наименование тегов.
        /// </summary>
        public string[] TagNames { get; set; }

        /// <summary>
        /// Наименование категории.
        /// </summary>
        public string CategoryName { get; set; }
    }
}
