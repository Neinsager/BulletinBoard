using System.ComponentModel.DataAnnotations;

namespace BulletinBoard.Contracts.Attributes;

/// <summary>
/// Валидирует размер массива тегов.
/// </summary>
public class TagsSizeAttribute : ValidationAttribute
{
    private readonly int _maxSize;
    private readonly int _minSize;

    /// <summary>
    /// Валидирует размер массива тегов.
    /// </summary>
    /// <param name="maxSize">Максимальная длина массива.</param>
    /// <param name="minSize">Минимальная длина массива.</param>
    /// <param name="errorMessage">Сообщение об ошибке.</param>
    public TagsSizeAttribute(int maxSize, int minSize = 1)
    {
        _maxSize = maxSize;
        _minSize = minSize;
    }

    /// <summary>
    /// Валидирует длину массива по максимальной и минимальной.
    /// </summary>
    /// <param name="value">Поле для валидации.</param>
    /// <param name="validationContext">Контекст валидации.</param>
    /// <returns>Результат валидации.</returns>
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value is null)
        {
            return ValidationResult.Success;
        }

        var tags = value as string[];
        if (tags.Length > _maxSize || tags.Length < _minSize)
        {
            return new ValidationResult(ErrorMessage);
        }

        return ValidationResult.Success;
    }
}