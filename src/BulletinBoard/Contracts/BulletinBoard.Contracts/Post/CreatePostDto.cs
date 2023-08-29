using System.ComponentModel.DataAnnotations;
using BulletinBoard.Contracts.Attributes;

namespace BulletinBoard.Contracts.Post;

/// <summary>
/// Создание объявления.
/// </summary>
public class CreatePostDto
{
    /// <summary>
    /// Заголовок.
    /// </summary>
    [Required]
    [StringLength(10, MinimumLength = 3, ErrorMessage = "Строка поля {0} должна быть по длине больше {2} и меньше {1} символов.")]
    public string Title { get; set; }

    /// <summary>
    /// Описание.
    /// </summary>
    [EmailAddress]
    public string Description { get; set; }

    /// <summary>
    /// Идентификатор категории.
    /// </summary>
    public Guid CategoryId { get; set; }

    /// <summary>
    /// Наименование тегов.
    /// </summary>
    [TagsSize(3, ErrorMessage = "Неверная длина массива")]
    public string[] TagNames { get; set; }

    /// <summary>
    /// Цена.
    /// </summary>
    [Range(0, 10_000, ErrorMessage = "Поле {0} должно быть больше {1} и меньше {2}.")]
    public decimal Price { get; set; }
}