using BulletinBoard.Contracts.Posts;
using BulletinBoard.Domain.Posts;

namespace BulletinBoard.Application.AppServices.Contexts.Posts.Repositories;

/// <summary>
/// Репозиторий для работы с объявлениями.
/// </summary>
public interface IPostRepository
{
    /// <summary>
    /// Получить постраничные объявления.
    /// </summary>
    /// <param name="pageSize">Размер страницы.</param>
    /// <param name="pageNumber">Номер страницы.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Список объявлений с краткой информацией <see cref="PostDto" />.</returns>
    Task<IReadOnlyCollection<PostInfoDto>> GetAllAsync(int pageSize, int pageNumber,
        CancellationToken cancellationToken);

    /// <summary>
    /// Возвращает объявление по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор объявления.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Модель объявления <see cref="PostDto"/></returns>
    Task<PostDto> GetByIdAsync(Guid id, CancellationToken cancellationToken);

    /// <summary>
    /// Создаёт объявление по модели.
    /// </summary>
    /// <param name="post">Модель объявления.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Идентификатор созданной сущности.</returns>
    Task<Guid> CreateAsync(Post post, CancellationToken cancellationToken);

    /// <summary>
    /// Обновить объявление по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор объявления.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Модель с обновленным объявлением <see cref="UpdatePostDto" />.</returns>
    Task<UpdatePostDto> UpdateByIdAsync(Guid id, CancellationToken cancellationToken);

    /// <summary>
    /// Удалить объявление по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор объявления</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    Task DeleteByIdAsync(Guid id, CancellationToken cancellationToken);
}