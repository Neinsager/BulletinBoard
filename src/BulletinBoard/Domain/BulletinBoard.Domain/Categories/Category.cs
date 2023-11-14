using BulletinBoard.Domain.Base;
using BulletinBoard.Domain.Posts;

namespace BulletinBoard.Domain.Categories
{
    /// <summary>
    /// Сущность категории.
    /// </summary>
    public class Category : BaseEntity
    {
        /// <summary>
        /// Название.
        /// </summary>
        public string CategoryName { get; set; }

        /// <summary>
        /// Родительская категория.
        /// </summary>
        public virtual Category? Parent { get; set; }

        /// <summary>
        /// Идентификатор родительской категории.
        /// </summary>
        public Guid? ParentId { get; init; }

        /// <summary>
        /// Дочерние категории.
        /// </summary>
        public virtual IEnumerable<Category>? SubCategories { get; set; }

        /// <summary>
        /// Объявления принадлежащие категории.
        /// </summary>
        public virtual IEnumerable<Post>? Posts { get; set; }
    }
}