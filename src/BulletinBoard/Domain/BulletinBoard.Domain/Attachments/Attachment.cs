using BulletinBoard.Domain.Base;
using BulletinBoard.Domain.Posts;

namespace BulletinBoard.Domain.Attachments
{
    /// <summary>
    /// Сущность вложения.
    /// </summary>
    public class Attachment : BaseEntity
    {
        /// <summary>
        /// Идентификатор объявления.
        /// </summary>
        public Guid PostId { get; set; }

        /// <summary>
        /// Объявление, к которому прикреплен файл.
        /// </summary>
        public virtual Post Post { get; set; }

        /// <summary>
        /// Содержимое вложения в виде массива байтов.
        /// </summary>
        public byte[] Content { get; set; }
    }
}
