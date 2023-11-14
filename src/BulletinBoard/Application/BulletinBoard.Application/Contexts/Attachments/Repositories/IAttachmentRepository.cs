using BulletinBoard.Contracts.Attachments;
using BulletinBoard.Domain.Attachments;
using System.Linq.Expressions;

namespace BulletinBoard.Application.AppServices.Contexts.Attachments.Repositories
{
    /// <summary>
    /// Репозиторий для работы с изображениями.
    /// </summary>
    public interface IAttachmentRepository
    {
        /// <summary>
        /// Возвращает изображение по заданному идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор изображения.</param>
        /// <param name="cancellationToken">Токен отмены операции.</param>
        /// <returns>Модель изображения <see cref="AttachmentDto"/>.</returns>
        Task<AttachmentDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken);

        /// <summary>
        /// Возвращает все изображения.
        /// </summary>
        /// <param name="cancellationToken">Токен отмены операции.</param>
        /// <returns>Коллекция изображений <see cref="AttachmentDto"/>.</returns>
        Task<IReadOnlyCollection<AttachmentDto>> GetAllAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Возвращает изображение по заданному условию.
        /// </summary>
        /// <param name="predicate">Предиката условие для фильтра.</param>
        /// <param name="cancellationToken">Токен отмены операции.</param>
        /// <returns>Модель объявления <see cref="Attachment"/>.</returns>
        Task<Attachment?> GetByPredicate(Expression<Func<Attachment, bool>> predicate, CancellationToken cancellationToken);

        /// <summary>
        /// Создает изображение.
        /// </summary>
        /// <param name="attachment">Изображение.</param>
        /// <param name="cancellationToken">Токен отмены операции.</param>
        /// <returns>Идентификатор созданной сущности.</returns>
        Task<Guid> CreateAsync(Attachment attachment, CancellationToken cancellationToken);

        /// <summary>
        /// Удаляет изображение по идентификатору.
        /// </summary>
        /// <param name="attachment">Изображение.</param>
        /// <param name="cancellationToken">Токен отмены операции.</param>
        Task DeleteAsync(Attachment attachment, CancellationToken cancellationToken);
    }
}