using BulletinBoard.Contracts.Comments;

namespace BulletinBoard.Application.AppServices.Contexts.Comments.Services
{
    /// <summary>
    /// Сервис для работы с комментариями.
    /// </summary>
    public interface ICommentService
    {
        /// <summary>
        /// Возвращает комментарий по заданному идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор комментария.</param>
        /// <param name="cancellationToken">Токен отмены операции.</param>
        /// <returns>Модель комментария <see cref="CommentDto"/>.</returns>
        Task<CommentDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken);

        /// <summary>
        /// Создает комментарий.
        /// </summary>
        /// <param name="dto">Модель комментария.</param>
        /// <param name="cancellationToken">Токен отмены операции.</param>
        /// <returns>Идентификатор созданной сущности.</returns>
        Task<Guid> CreateAsync(CreateCommentDto dto, CancellationToken cancellationToken);

        /// <summary>
        /// Редактирует комментарий.
        /// </summary>
        /// <param name="id">Идентификатор комментария.</param>
        /// <param name="dto">Модель комментария.</param>
        /// <param name="cancellationToken">Токен отмены операции.</param>
        Task UpdateAsync(Guid id, UpdateCommentDto dto, CancellationToken cancellationToken);

        /// <summary>
        /// Удаляет комментарий по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор комментария.</param>
        /// <param name="cancellationToken">Токен отмены операции.</param>
        Task DeleteAsync(Guid id, CancellationToken cancellationToken);
    }
}
