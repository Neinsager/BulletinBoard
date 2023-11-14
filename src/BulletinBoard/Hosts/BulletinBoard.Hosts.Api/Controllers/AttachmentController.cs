using BulletinBoard.Application.AppServices.Authentication.Constants;
using BulletinBoard.Application.AppServices.Contexts.Attachments.Services;
using BulletinBoard.Application.AppServices.Exceptions;
using BulletinBoard.Contracts.Attachments;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace BulletinBoard.Hosts.Api.Controllers
{
    /// <summary>
    /// Контроллер для работы с изображениями.
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class AttachmentController : ControllerBase
    {
        private readonly IAttachmentService _attachmentService;
        private readonly ILogger<AttachmentController> _logger;
        private readonly IMemoryCache _memoryCache;

        /// <summary>
        /// Инициализирует экземпляр <see cref="AttachmentController"/>.
        /// </summary>
        /// <param name="attachmentService">Сервис работы с изображениями.</param>
        /// <param name="logger">Логирование работы с изображениями.</param>
        /// <param name="memoryCache">Кеширование изображений.</param>
        public AttachmentController(IAttachmentService attachmentService, ILogger<AttachmentController> logger, IMemoryCache memoryCache)
        {
            _attachmentService = attachmentService;
            _logger = logger;
            _memoryCache = memoryCache;
        }

        /// <summary>
        /// Возвращает список всех изображений.
        /// </summary>
        /// <param name="cancellationToken">Отмена операции.</param>
        /// <returns></returns>
        [Authorize]
        [Authorize(Roles = AuthRoles.Admin)]
        [HttpGet("get-all")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAllAsync(CancellationToken cancellationToken)
        {
            var result = await _attachmentService.GetAllAsync(cancellationToken);
            return Ok(result);
        }

        /// <summary>
        /// Возвращает изображение по заданному идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор.</param>
        /// <param name="cancellationToken">Отмена операции.</param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet("get-by-id")]
        [ProducesResponseType(typeof(AttachmentDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var cacheKey = $"Post_{id}";
            if (!_memoryCache.TryGetValue(cacheKey, out var result))
            {
                var attachment = await _attachmentService.GetByIdAsync(id, cancellationToken);
                if (attachment != null)
                {
                    result = await _memoryCache.GetOrCreateAsync(cacheKey, async entry =>
                    {
                        entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(15);
                        entry.Priority = CacheItemPriority.Low;
                        _logger.LogInformation($"Изображение по '{id}' успешно получено с сервера и сохранено в кэш.");
                        return attachment;
                    });
                }
            }
            else
            {
                _logger.LogInformation($"Изображение по '{id}' успешно получено c кэша.");
            }
            if (result == null)
                return NotFound();
            return Ok(result);
        }

        /// <summary>
        /// Создаёт изображение.
        /// </summary>
        /// <param name="dto">Модель изображения.</param>
        /// <param name="cancellationToken">Отмена операции.</param>
        /// <returns></returns>
        [Authorize]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> CreateAsync([FromForm] CreateAttachmentDto dto, CancellationToken cancellationToken)
        {
            var dtoId = await _attachmentService.CreateAsync(dto, cancellationToken);
            return Created(nameof(CreateAsync), dtoId);
        }

        /// <summary>
        /// Удаляет изображение по идентификатору.
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
                await _attachmentService.DeleteAsync(id, cancellationToken);
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