using BulletinBoard.Application.AppServices.Contexts.Posts.Services;
using BulletinBoard.Application.AppServices.Exceptions;
using BulletinBoard.Contracts.Posts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace BulletinBoard.Hosts.Api.Controllers
{
    /// <summary>
    /// Контроллер для работы с объявлениями.
    /// </summary>
    [ApiController]
    [Route("post")]
    public class PostController : ControllerBase
    {
        private readonly IPostService _postService;
        private readonly ILogger<PostController> _logger;
        private readonly IMemoryCache _memoryCache;

        /// <summary>
        /// Инициализирует экзепляр <see cref="PostController"/>
        /// </summary>
        /// <param name="postService">Сервис работы с объявлениями.</param>
        /// <param name="logger">Логирование работы с объявлениями.</param>
        public PostController(IPostService postService,
            ILogger<PostController> logger,
            IMemoryCache memoryCache)
        {
            _postService = postService;
            _logger = logger;
            _memoryCache = memoryCache;
        }

        /// <summary>
        /// Получает объявление по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор объявления.</param>
        /// <param name="cancellationToken">Отмена операции.</param>
        /// <returns>Модель объявления.<see cref="PostDto"/></returns>
        [ProducesResponseType(typeof(PostInfoDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet("{id:guid}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var cacheKey = $"Ad_{id}";

            if (!_memoryCache.TryGetValue(cacheKey, out var result))
            {
                var post = await _postService.GetByIdAsync(id, cancellationToken);
                if (post != null)
                {
                    result = await _memoryCache.GetOrCreateAsync(cacheKey, async entry =>
                    {
                        entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(15);
                        entry.Priority = CacheItemPriority.Low;
                        _logger.LogInformation("Объявление по {id} успешно получено с сервера и сохранено в кэш.");
                        return post;
                    });
                }
            }
            else
            {
                _logger.LogInformation("Объявление по {id} успешно получено c кэша.");
            }
            if (result == null) return NotFound(result);
            return Ok(result);
        }

        /// <summary>
        /// Возращает постраничные объявления.
        /// </summary>
        /// <param name="cancellationToken">Отмена операции.</param>
        /// <param name="pageSize">Размер страницы.</param>
        /// <param name="pageIndex">Индекс страницы.</param>
        /// <returns>Коллекция объвлений.<see cref="PostDto"/></returns>
        [HttpGet]
        [AllowAnonymous]
        [ProducesResponseType(typeof(PostInfoDto[]), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllAsync(CancellationToken cancellationToken, int pageSize = 20, int pageIndex = 0)
        {
            {
                _logger.LogInformation("Запрос списка объявлений.");

                var result = await _postService.GetAllAsync(pageSize, pageIndex, cancellationToken);

                _logger.LogInformation("Список объявлений успешно получен.");

                return Ok(result);
            }
        }

        /// <summary>
        /// Создает объявления.
        /// </summary>
        /// <param name="dto">Модель создании объявления.</param>
        /// <param name="cancellationToken">Отмена операции.</param>
        [HttpPost]
        public async Task<IActionResult> CreateAsync(CreatePostDto dto, CancellationToken cancellationToken)
        {
            var modelId = await _postService.CreateAsync(dto, cancellationToken);
            return Created(nameof(CreateAsync), modelId);
        }

        /// <summary>
        /// Редактирует объвления.
        /// </summary>
        /// <param name="id">Идентификатор объявления.</param>
        /// <param name="dto">Модель редактирования объявления.</param>
        /// <param name="cancellationToken">Отмена операции.</param>
        [Authorize]
        [HttpPut("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateByIdAsync(Guid id, PostDto dto, CancellationToken cancellationToken)
        {
            var cacheKey = $"Post_{id}";
            if (_memoryCache.TryGetValue(cacheKey, out var result))
                _memoryCache.Remove(cacheKey);
            try
            {
                await _postService.UpdateByIdAsync(id, cancellationToken);
            }
            catch (EntityNotFoundException ex)
            {
                ModelState.AddModelError("NotFoundError", ex.Message);
                return NotFound(ModelState);
            }
            return NoContent();
        }

        /// <summary>
        /// Удаляет объвления по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор объявления.</param>
        /// <param name="cancellationToken">Отмена операции.</param>
        [Authorize]
        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            try
            {
                await _postService.DeleteByIdAsync(id, cancellationToken);
            }
            catch (EntityNotFoundException ex)
            {
                ModelState.AddModelError("NotFoundError", ex.Message);
                return NotFound(ModelState);
            }
            return NoContent();
        }
    }
}