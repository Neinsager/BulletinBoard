using AutoMapper;
using BulletinBoard.Application.AppServices.Contexts.Attachments.Repositories;
using BulletinBoard.Contracts.Attachments;
using BulletinBoard.Domain.Attachments;
using BulletinBoard.Infrastructure.Repository;
using System.Linq.Expressions;

namespace BulletinBoard.Infrastructure.DataAccess.Contexts.Attachments.Repositories
{
    /// <inheritdoc cref="IAttachmentRepository"/>
    public class AttachmentRepository : IAttachmentRepository
    {
        private readonly IRepository<Attachment> _repository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Инициализирует репозиторий изображений.
        /// </summary>
        /// <param name="repository">Репозиторий.</param>

        public AttachmentRepository(IRepository<Attachment> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        /// <inheritdoc/>
        public Task<IReadOnlyCollection<AttachmentDto>> GetAllAsync(CancellationToken cancellationToken)
        {
            var allCollection = _repository.GetAll();
            var dtoCollection = _mapper.Map<List<AttachmentDto>>(allCollection.ToList());
            IReadOnlyCollection<AttachmentDto> readonlyCollection = dtoCollection.AsReadOnly();

            return Task.Run(() => readonlyCollection, cancellationToken);
        }

        /// <inheritdoc/>
        public Task<AttachmentDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return _repository.GetByIdAsync(id, cancellationToken).ContinueWith(t =>
            {
                var attachment = t.Result;
                return _mapper.Map<AttachmentDto?>(attachment);
            });
        }

        /// <inheritdoc/>
        public Task<Attachment?> GetByPredicate(Expression<Func<Attachment, bool>> predicate, CancellationToken cancellationToken)
        {
            return Task.Run(() => (_repository.GetAllFiltered(predicate).FirstOrDefault()), cancellationToken);
        }

        /// <inheritdoc/>
        public Task<Guid> CreateAsync(Attachment attachment, CancellationToken cancellationToken)
        {
            _repository.AddAsync(attachment, cancellationToken);
            return Task.Run(() => attachment.Id, cancellationToken);
        }

        /// <inheritdoc/>
        public Task DeleteAsync(Attachment attachment, CancellationToken cancellationToken)
        {
            return _repository.DeleteAsync(attachment, cancellationToken);
        }
    }
}