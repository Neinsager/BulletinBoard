using BulletinBoard.Contracts.Comments;
using BulletinBoard.Domain.Comments;
using System.Linq.Expressions;

namespace BulletinBoard.Application.AppServices.Contexts.Comments.Repositories
{
    /// <summary>
    /// Репозиторий для работы с комментариями.
    /// </summary>
    public interface ICommentRepository
    {
        /// <summary>
        /// Возвращает комментарий по заданному идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор комментария.</param>
        /// <param name="cancellationToken">Токен отмены операции.</param>
        /// <returns>Модель комментария <see cref="CommentDto"/>.</returns>
        Task<CommentDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken);

        /// <summary>
        /// Возвращает комментарий по заданному условию.
        /// </summary>
        /// <param name="predicate">Предиката условие для фильтра.</param>
        /// <param name="cancellationToken">Токен отмены операции.</param>
        /// <returns>Модель комментария <see cref="Comment"/>.</returns>
        Task<Comment?> GetByPredicate(Expression<Func<Comment, bool>> predicate, CancellationToken cancellationToken);

        /// <summary>
        /// Создает комментарий.
        /// </summary>
        /// <param name="comment">Комментарий.</param>
        /// <param name="cancellationToken">Токен отмены операции.</param>
        /// <returns>Идентификатор созданной сущности.</returns>
        Task<Guid> CreateAsync(Comment comment, CancellationToken cancellationToken);

        /// <summary>
        /// Редактирует комментарий.
        /// </summary>
        /// <param name="id">Идентификатор комментария.</param>
        /// <param name="comment">Комментарий.</param>
        /// <param name="cancellationToken">Токен отмены операции.</param>
        Task UpdateAsync(Guid id, Comment comment, CancellationToken cancellationToken);

        /// <summary>
        /// Удаляет комментарий по идентификатору.
        /// </summary>
        /// <param name="comment">Комментарий.</param>
        /// <param name="cancellationToken">Токен отмены операции.</param>
        Task DeleteAsync(Comment comment, CancellationToken cancellationToken);
    }
}
