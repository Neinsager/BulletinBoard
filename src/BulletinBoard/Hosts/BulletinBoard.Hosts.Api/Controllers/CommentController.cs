using BulletinBoard.Application.AppServices.Contexts.Comments.Services;
using BulletinBoard.Application.AppServices.Exceptions;
using BulletinBoard.Contracts.Comments;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace BulletinBoard.Hosts.Api.Controllers
{
    /// <summary>
    /// Контроллер для работы с комментариями.
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class CommentController : ControllerBase
    {
        private readonly ICommentService _commentService;
        private readonly ILogger<CommentController> _logger;
        private readonly IMemoryCache _memoryCache;

        /// <summary>
        /// Инициализирует экземпляр <see cref="CommentController"/> 
        /// </summary>
        /// <param name="commentService"></param>
        /// <param name="logger"></param>
        /// <param name="memoryCache"></param>
        public CommentController(ICommentService commentService, ILogger<CommentController> logger, IMemoryCache memoryCache)
        {
            _commentService = commentService;
            _logger = logger;
            _memoryCache = memoryCache;
        }

        /// <summary>
        /// Возвращает комментарий по заданному идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор.</param>
        /// <param name="cancellationToken">Отмена операции.</param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet("get-by-id")]
        [ProducesResponseType(typeof(CommentDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var cacheKey = $"Comment_{id}";
            if (!_memoryCache.TryGetValue(cacheKey, out var result))
            {
                var comment = await _commentService.GetByIdAsync(id, cancellationToken);
                if (comment != null)
                {
                    result = await _memoryCache.GetOrCreateAsync(cacheKey, async entry =>
                    {
                        entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(15);
                        entry.Priority = CacheItemPriority.Low;

                        _logger.LogInformation($"Комментарий с идентификатором {id} был успешно получен из сервиса и сохранен в кеше.");
                        return comment;
                    });
                }
            }
            else
            {
                _logger.LogInformation($"Комментарий с идентификатором {id} успешно получен из кэша.");
            }
            if (result == null)
                return NotFound();
            return Ok(result);
        }

        /// <summary>
        /// Создаёт комментарий.
        /// </summary>
        /// <param name="dto">Модель комментария.</param>
        /// <param name="cancellationToken">Отмена операции.</param>
        /// <returns></returns>
        [Authorize]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateAsync([FromBody] CreateCommentDto dto, CancellationToken cancellationToken)
        {
            var dtoId = await _commentService.CreateAsync(dto, cancellationToken);

            return Created(nameof(CreateAsync), dtoId);
        }

        /// <summary>
        /// Редактирует комментарий.
        /// </summary>
        /// <param name="id">Идентификатор комментария.</param>
        /// <param name="dto">Модель комментария.</param>
        /// <param name="cancellationToken">Отмена операции.</param>
        [Authorize]
        [HttpPut("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateAsync(Guid id, UpdateCommentDto dto, CancellationToken cancellationToken)
        {
            try
            {
                await _commentService.UpdateAsync(id, dto, cancellationToken);
            }
            catch (EntityNotFoundException ex)
            {
                ModelState.AddModelError("NotFoundError", ex.Message);
                return NotFound(ModelState);
            }
            return NoContent();
        }

        /// <summary>
        /// Удаляет комментарий по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор.</param>
        /// <param name="cancellationToken">Отмена операции.</param>
        /// <returns></returns>
        [Authorize]
        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            try
            {
                await _commentService.DeleteAsync(id, cancellationToken);
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