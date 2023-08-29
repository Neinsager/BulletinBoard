using BulletinBoard.Contracts;
using BulletinBoard.Contracts.Post;

namespace BulletinBoard.Application.AppServices.Contexts.Post.Services;

/// <summary>
/// Сервис работы с объявлениями.
/// </summary>
public interface IPostService
{
    /// <summary>
    /// Возвращает объявление по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор объявления.</param>
    /// <param name="cancellationToken">Отмена операции.</param>
    /// <returns>Модель объявления.<see cref="PostDto"/></returns>
    Task<PostDto> GetByIdAsync(Guid id, CancellationToken cancellationToken);

    /// <summary>
    /// Создаёт объявление по модели.
    /// </summary>
    /// <param name="model">Модель объявления.</param>
    /// <param name="cancellationToken">Отмена операции.</param>
    /// <returns>Идентификатор созданной сущности.</returns>
    Task<Guid> CreateAsync(CreatePostDto model, CancellationToken cancellationToken);
}