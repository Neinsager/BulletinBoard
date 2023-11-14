using AutoMapper;
using BulletinBoard.Application.AppServices.Contexts.Posts.Repositories;
using BulletinBoard.Contracts.Posts;
using BulletinBoard.Domain.Posts;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Security.Claims;

namespace BulletinBoard.Application.AppServices.Contexts.Posts.Services;

/// <inheritdoc />
public class PostService : IPostService
{
    private readonly IPostRepository _postRepository;
    private readonly ILogger<PostService> _logger;
    private readonly IMapper _mapper;
    private readonly IHttpContextAccessor _httpContextAccessor;

    /// <summary>
    /// Инициализирует экзепляр <see cref="PostService"/>
    /// </summary>
    /// <param name="postRepository">Репозиторий для работы с объявлениями.</param>
    /// <param name="logger">Сервис логгирования.</param>
    /// <param name="mapper">Маппер.</param>
    /// <param name="httpContextAccessor">Предоставляет доступ к текущему <see cref="HttpContext"/>, если он доступен.</param>
    public PostService(IPostRepository postRepository, ILogger<PostService> logger, 
        IMapper mapper, IHttpContextAccessor httpContextAccessor)
    {
        _postRepository = postRepository;
        _logger = logger;
        _mapper = mapper;
        _httpContextAccessor = httpContextAccessor;
    }

    /// <inheritdoc/>
    public async Task<IReadOnlyCollection<PostDto>> GetAllAsync(int pageSize, int pageNumber,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation("Получение коллекции объявлений.");
        var postsDto = await _postRepository.GetAllAsync(pageNumber, pageSize, cancellationToken);
        _logger.LogInformation("Коллекция объявлений успешно получена.");
        return postsDto;
    }

    /// <inheritdoc />
    public async Task<PostDto> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"Получение объявления под Id: {id}");
        var existingPost = await _postRepository.GetByIdAsync(id, cancellationToken);
        return existingPost;
    }

    /// <inheritdoc/>
    public Task<Guid> CreateAsync(CreatePostDto dto, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"Создание объявления {dto.Title}");

        var post = _mapper.Map<Post>(dto);
        var userId = _httpContextAccessor.HttpContext!.User.FindFirstValue(ClaimTypes.NameIdentifier);
        post.UserId = Guid.Parse(userId!);

        return _postRepository.CreateAsync(post, cancellationToken);
    }

    /// <inheritdoc/>
    public async Task<UpdatePostDto> UpdateByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"Изменение объявления под Id: {id}");
        var updatedPost = await _postRepository.UpdateByIdAsync(id, cancellationToken);
        return updatedPost;
    }

    /// <inheritdoc/>
    public async Task DeleteByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"Удаление объявления под Id: {id}");
        await _postRepository.DeleteByIdAsync(id, cancellationToken);
    }
}