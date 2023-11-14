using BulletinBoard.Contracts.Base;
using BulletinBoard.Contracts.Posts;

namespace BulletinBoard.Contracts.Categories
{
    /// <summary>
    /// Категория.
    /// </summary>
    public class CategoryDto : BaseDto
    {
        /// <summary>
        /// Название категории.
        /// </summary>
        public string CategoryName { get; init; }

        /// <summary>
        /// Родительская категория.
        /// </summary>
        public CategoryDto? Parent { get; set; }

        /// <summary>
        /// Идентификатор родительской категории.
        /// </summary>
        public Guid? ParentId { get; init; }

        /// <summary>
        /// Список подкатегорий.
        /// </summary>
        public IEnumerable<CategoryDto> SubCategories { get; set; }

        /// <summary>
        /// Объявления принадлежащие категории.
        /// </summary>
        public IEnumerable<PostDto>? Posts { get; set; }
    }
}
