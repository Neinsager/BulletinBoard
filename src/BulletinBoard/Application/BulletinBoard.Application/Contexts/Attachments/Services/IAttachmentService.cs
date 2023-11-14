using BulletinBoard.Contracts.Attachments;

namespace BulletinBoard.Application.AppServices.Contexts.Attachments.Services
{
    /// <summary>
    /// Сервис работы с изображениями.
    /// </summary>
    public interface IAttachmentService
    {
        /// <summary>
        /// Возвращает все изображения.
        /// </summary>
        /// <param name="cancellationToken">Токен отмены операции.</param>
        /// <returns>Коллекция изображений <see cref="AttachmentDto"/></returns>
        Task<IReadOnlyCollection<AttachmentDto>> GetAllAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Возвращает изображение по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор изображения.</param>
        /// <param name="cancellationToken">Токен отмены операции.</param>
        /// <returns>Модель изображения <see cref="AttachmentDto"/></returns>
        Task<AttachmentDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken);

        /// <summary>
        /// Создает изображение.
        /// </summary>
        /// <param name="dto">Модель создания изображения.</param>
        /// <param name="cancellationToken">Токен отмены операции.</param>
        /// <returns>Идентификатор созданной сущности.</returns>
        Task<Guid> CreateAsync(CreateAttachmentDto dto, CancellationToken cancellationToken);

        /// <summary>
        /// Удаляет изображение по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор изображения.</param>
        /// <param name="cancellationToken">Токен отмены операции.</param>
        Task DeleteAsync(Guid id, CancellationToken cancellationToken);
    }
}