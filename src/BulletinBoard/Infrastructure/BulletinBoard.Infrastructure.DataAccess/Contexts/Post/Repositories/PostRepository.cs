using AutoMapper;
using BulletinBoard.Application.AppServices.Contexts.Posts.Repositories;
using BulletinBoard.Contracts.Posts;
using BulletinBoard.Infrastructure.Repository;
using BulletinBoard.Domain.Posts;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace BulletinBoard.Infrastructure.DataAccess.Contexts.Posts.Repositories;

/// <inheritdoc cref="IPostRepository"/>
public class PostRepository : IPostRepository
{
    private readonly IRepository<Post> _repository;
    private readonly IMapper _mapper;

    /// <summary>
    /// Конструктор репозитория объявлений.
    /// </summary>
    /// <param name="repository">Базовый репозиторий.</param>
    /// <param name="mapper">Маппер.</param>
    public PostRepository(IRepository<Post> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    /// <inheritdoc/>
    public async Task<Guid> CreateAsync(Post post, CancellationToken cancellationToken)
    {
        await _repository.AddAsync(post, cancellationToken);
        return post.Id;
    }

    /// <inheritdoc/>
    public Task DeleteByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var dto = FindById(id, cancellationToken);
        var post = _mapper.Map<Post>(dto);
        return _repository.DeleteAsync(post, cancellationToken);
    }

    /// <inheritdoc/>
    public async Task<IReadOnlyCollection<PostInfoDto>> GetAllAsync(int pageSize, 
        int pageNumber, CancellationToken cancellationToken)
    {
        var postsList = await _repository.GetAllFiltered(post => true)
            .OrderBy(post => post.Title)
            .Skip(pageNumber * pageSize)
            .Take(pageSize)
            .ProjectTo<PostInfoDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);
        return postsList;
    }

    /// <inheritdoc/>
    public async Task<PostDto> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var post = await FindById(id, cancellationToken);
        var dto = _mapper.Map<PostDto>(post);
        return dto;
    }

    /// <inheritdoc/>
    public async Task<UpdatePostDto> UpdateByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var dto = FindById(id, cancellationToken);
        var post = _mapper.Map<Post>(dto);
        await _repository.UpdateAsync(post, cancellationToken);
        return _mapper.Map<UpdatePostDto>(post);
    }

    /// <summary>
    /// Метод для поиска объявлений по id.
    /// </summary>
    /// <param name="id">Id объявления.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Модель объявления.</returns>
    private async Task<Post> FindById(Guid id, CancellationToken cancellationToken)
    {
        var advertisement = await _repository.GetByIdAsync(id, cancellationToken);
        return advertisement;
    }
}