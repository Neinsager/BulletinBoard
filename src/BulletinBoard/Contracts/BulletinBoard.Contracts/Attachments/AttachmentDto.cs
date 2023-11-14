using BulletinBoard.Contracts.Base;
using BulletinBoard.Contracts.Posts;
using System.ComponentModel.DataAnnotations;

namespace BulletinBoard.Contracts.Attachments;

/// <summary>
/// Изображение.
/// </summary>
public class AttachmentDto : BaseDto
{
    /// <summary>
    /// Изображение в виде массива байтов.
    /// </summary>
    [Required]
    public byte[] Content { get; set; }

    /// <summary>
    /// Объявление которому принадлежит изображение.
    /// </summary>
    [Required]
    public PostDto Post { get; set; }
}