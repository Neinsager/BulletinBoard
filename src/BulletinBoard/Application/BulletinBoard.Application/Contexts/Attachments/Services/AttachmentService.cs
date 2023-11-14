using AutoMapper;
using BulletinBoard.Application.AppServices.Authentication.Constants;
using BulletinBoard.Application.AppServices.Authentication.EntitiesAuth;
using BulletinBoard.Application.AppServices.Contexts.Attachments.Repositories;
using BulletinBoard.Application.AppServices.Exceptions;
using BulletinBoard.Contracts.Attachments;
using BulletinBoard.Domain.Attachments;
using Microsoft.AspNetCore.Http;

namespace BulletinBoard.Application.AppServices.Contexts.Attachments.Services
{
    /// <inheritdoc cref="IAttachmentService"/>
    public class AttachmentService : IAttachmentService
    {
        private readonly IAttachmentRepository _attachmentRepository;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IEntityAuthorizationService _entityAuthorizationService;

        /// <summary>
        /// Инициализирует <see cref="AttachmentService"/>
        /// </summary>
        /// <param name="attachmentRepository">Репозиторий изображения.</param>
        /// <param name="mapper">Маппер.</param>
        /// <param name="httpContextAccessor">Предоставляет доступ к текущему <see cref="HttpContext"/>, если он доступен.</param>
        /// <param name="entityAuthorizationService">Сервис для проверки прав доступа пользователя к сущности.</param>
        public AttachmentService(IAttachmentRepository attachmentRepository, IMapper mapper,
            IHttpContextAccessor httpContextAccessor, IEntityAuthorizationService entityAuthorizationService)
        {
            _attachmentRepository = attachmentRepository;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _entityAuthorizationService = entityAuthorizationService;
        }

        /// <inheritdoc/>
        public Task<IReadOnlyCollection<AttachmentDto>> GetAllAsync(CancellationToken cancellationToken)
        {
            return _attachmentRepository.GetAllAsync(cancellationToken);
        }

        /// <inheritdoc/>
        public Task<AttachmentDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return _attachmentRepository.GetByIdAsync(id, cancellationToken);
        }

        /// <inheritdoc/>
        public Task<Guid> CreateAsync(CreateAttachmentDto dto, CancellationToken cancellationToken)
        {
            var attachment = _mapper.Map<Attachment>(dto);

            return _entityAuthorizationService.Validate(_httpContextAccessor.HttpContext!.User, dto.PostId, AuthRoles.Admin).ContinueWith(t =>
            {
                if (t.Result) throw new EntityForbiddenException();
                return _attachmentRepository.CreateAsync(attachment, cancellationToken);
            }).Unwrap();
        }

        /// <inheritdoc/>
        public Task DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            var attachment = _attachmentRepository.GetByPredicate(a => a.Id == id, cancellationToken).Result ?? throw new EntityNotFoundException();

            return _entityAuthorizationService.Validate(_httpContextAccessor.HttpContext!.User, attachment.PostId, AuthRoles.Admin).ContinueWith(t =>
            {
                if (t.Result) throw new EntityForbiddenException();
                return _attachmentRepository.DeleteAsync(attachment, cancellationToken);
            }).Unwrap();
        }
    }
}