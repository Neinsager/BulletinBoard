using BulletinBoard.Contracts.Base;

namespace BulletinBoard.Contracts.Categories;

/// <summary>
/// Информации о категории.
/// </summary>
public class CategoryInfoDto : BaseDto
{
    /// <summary>
    /// Название категории.
    /// </summary>
    public string CategoryName { get; init; }

    /// <summary>
    /// Идентификатор родительской категории.
    /// </summary>
    public Guid? ParentId { get; init; }
}