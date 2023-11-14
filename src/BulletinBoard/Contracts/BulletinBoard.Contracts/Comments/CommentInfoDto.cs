using BulletinBoard.Contracts.Base;

namespace BulletinBoard.Contracts.Comments
{
    /// <summary>
    /// Информация об комментарии.
    /// </summary>
    public class CommentInfoDto : BaseDto
    {
        /// <summary>
        /// Содержимое комментария.
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// Идентификатор объявления которому принадлежит комментарий.
        /// </summary>
        public Guid PostId { get; set; }
    }
}
