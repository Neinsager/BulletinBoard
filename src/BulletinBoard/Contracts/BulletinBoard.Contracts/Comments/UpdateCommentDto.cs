using System.ComponentModel.DataAnnotations;

namespace BulletinBoard.Contracts.Comments
{
    public class UpdateCommentDto
    {
        /// <summary>
        /// Идентификатор комментария.
        /// </summary>
        [Required]
        public Guid CommentId { get; set; }

        /// <summary>
        /// Содержимое комментария.
        /// </summary>
        [Required]
        public string Content { get; set; }
    }
}
