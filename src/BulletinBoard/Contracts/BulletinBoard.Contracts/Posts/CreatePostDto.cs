﻿using System.ComponentModel.DataAnnotations;

namespace BulletinBoard.Contracts.Posts;

/// <summary>
/// Создание объявления.
/// </summary>
public class CreatePostDto
{
    /// <summary>
    /// Заголовок.
    /// </summary>
    [Required(ErrorMessage = "Поле {0} не может быть пустым.")]
    [StringLength(100, MinimumLength = 5, ErrorMessage = "Поле {0} должно содержать от {2} до {1} символов.")]
    public string Title { get; set; }

    /// <summary>
    /// Описание.
    /// </summary>
    [Required(ErrorMessage = "Поле {0} не может быть пустым.")]
    [StringLength(500, MinimumLength = 20, ErrorMessage = "Поле {0} должно содержать от {2} до {1} символов.")]
    public string Description { get; set; }

    /// <summary>
    /// Идентификатор категории.
    /// </summary>
    [Required]
    public Guid CategoryId { get; set; }

    /// <summary>
    /// Наименование тегов.
    /// </summary>
    [Required]
    [MaxLength(6, ErrorMessage = "Максимальное количество {0}: {1}")]
    [MinLength(1, ErrorMessage = "Минимальное количество {0}: {1}")]
    public string[] TagNames { get; set; }

    /// <summary>
    /// Цена.
    /// </summary>
    [Required(ErrorMessage = "Поле {0} не может быть пустым.")]
    [Range(0, 100_000_000_000, ErrorMessage = "Поле {0} должно быть в диапазоне от {1} до {2}.")]
    public decimal Price { get; set; }
}