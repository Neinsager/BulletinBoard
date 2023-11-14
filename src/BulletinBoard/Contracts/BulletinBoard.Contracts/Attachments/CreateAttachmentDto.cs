using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace BulletinBoard.Contracts.Attachments
{
    /// <summary>
    /// Создание изображения.
    /// </summary>
    public class CreateAttachmentDto
    {
        /// <summary>
        /// Изображение в виде массива байтов.
        /// </summary>
        [Display(Name = "Изображение")]
        [Required(ErrorMessage = "Выберите изображение!")]
        public IFormFile File { get; set; }

        /// <summary>
        /// Идентификатор объявления которому принадлежит изображение.
        /// </summary>
        [Required]
        public Guid PostId { get; set; }
    }
}
