using System.ComponentModel.DataAnnotations;

namespace BulletinBoard.Contracts.Comments
{
    /// <summary>
    /// Создание комментария.
    /// </summary>
    public class CreateCommentDto
    {
        /// <summary>
        /// Содержимое комментария.
        /// </summary>
        [Required]
        [StringLength(maximumLength: 1000)]
        public string Content { get; set; }

        /// <summary>
        /// Идентификатор объявления которому принадлежит комментарий.
        /// </summary>
        [Required]
        public Guid PostId { get; set; }
    }
}
