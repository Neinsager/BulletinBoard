using System.ComponentModel.DataAnnotations;

namespace BulletinBoard.Contracts.Categories
{
    /// <summary>
    /// Создания категории.
    /// </summary>
    public class CreateCategoryDto
    {
        /// <summary>
        /// Название категории.
        /// </summary>
        [Required(ErrorMessage = "Поле {0} не может быть пустым.")]
        [StringLength(100, MinimumLength = 4, ErrorMessage = "Поле {0} должно содержать от {2} до {1} символов.")]
        public string CategoryName { get; set; }

        /// <summary>
        /// Идентификатор родительской категории.
        /// </summary>
        public Guid ParentId { get; set; }
    }
}
