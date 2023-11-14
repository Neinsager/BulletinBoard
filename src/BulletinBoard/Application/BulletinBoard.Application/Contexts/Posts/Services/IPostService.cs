using BulletinBoard.Contracts.Posts;

namespace BulletinBoard.Application.AppServices.Contexts.Posts.Services
{
    /// <summary>
    /// Сервис работы с объявлениями.
    /// </summary>
    public interface IPostService
    {
        /// <summary>
        /// Получить постраничные объявления.
        /// </summary>
        /// <param name="pageSize">Размер страницы.</param>
        /// <param name="pageNumber">Номер страницы.</param>
        /// <param name="cancellationToken">Токен отмены операции.</param>
        /// <returns>Список объявлений с краткой информацией <see cref="PostDto" />.</returns>
        Task<IReadOnlyCollection<PostDto>> GetAllAsync(int pageSize, int pageNumber,
            CancellationToken cancellationToken);

        /// <summary>
        /// Возвращает объявление по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор объявления.</param>
        /// <param name="cancellationToken">Токен отмены операции.</param>
        /// <returns>Модель объявления.<see cref="PostDto"/></returns>
        Task<PostDto> GetByIdAsync(Guid id, CancellationToken cancellationToken);

        /// <summary>
        /// Создаёт объявление по модели.
        /// </summary>
        /// <param name="model">Модель объявления.</param>
        /// <param name="cancellationToken">Токен отмены операции.</param>
        /// <returns>Идентификатор созданной сущности.</returns>
        Task<Guid> CreateAsync(CreatePostDto model, CancellationToken cancellationToken);

        /// <summary>
        /// Обновить объявление по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор объявления</param>
        /// <param name="cancellationToken">Токен отмены операции.</param>
        /// <returns>Модель с обновленным объявлением <see cref="UpdatePostDto" />.</returns>
        Task<UpdatePostDto> UpdateByIdAsync(Guid id, CancellationToken cancellationToken);

        /// <summary>
        /// Удалить объявление по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор объявления.</param>
        /// <param name="cancellationToken">Токен отмены операции.</param>
        Task DeleteByIdAsync(Guid id, CancellationToken cancellationToken);
    }
}