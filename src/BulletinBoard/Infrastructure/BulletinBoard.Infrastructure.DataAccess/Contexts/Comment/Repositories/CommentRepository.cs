using AutoMapper;
using BulletinBoard.Application.AppServices.Contexts.Comments.Repositories;
using BulletinBoard.Contracts.Comments;
using BulletinBoard.Domain.Comments;
using BulletinBoard.Infrastructure.Repository;
using System.Linq.Expressions;

namespace BulletinBoard.Infrastructure.DataAccess.Contexts.Comments.Repositories
{
    /// <inheritdoc cref="ICommentRepository"/>
    public class CommentRepository : ICommentRepository
    {
        private readonly IRepository<Comment> _repository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Инициализация репозитория <see cref="CommentRepository"/>
        /// </summary>
        /// <param name="repository"> Репозиторий.</param>
        /// <param name="mapper">Маппер.</param>
        public CommentRepository(IRepository<Comment> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        /// <inheritdoc/>
        public Task<CommentDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return _repository.GetByIdAsync(id, cancellationToken).ContinueWith(t =>
            {
                var comment = t.Result;
                return _mapper.Map<CommentDto?>(comment);
            });
        }

        /// <inheritdoc/>
        public Task<Comment?> GetByPredicate(Expression<Func<Comment, bool>> predicate, CancellationToken cancellationToken)
        {
            return Task.Run(() => (_repository.GetAllFiltered(predicate).FirstOrDefault()), cancellationToken);
        }

        /// <inheritdoc/>
        public Task<Guid> CreateAsync(Comment comment, CancellationToken cancellationToken)
        {
            _repository.AddAsync(comment, cancellationToken);
            return Task.FromResult(comment.Id);
        }

        /// <inheritdoc/>
        public Task UpdateAsync(Guid id, Comment comment, CancellationToken cancellationToken)
        {
            return _repository.UpdateAsync(comment, cancellationToken);
        }

        /// <inheritdoc/>
        public Task DeleteAsync(Comment comment, CancellationToken cancellationToken)
        {
            return _repository.DeleteAsync(comment, cancellationToken);
        }
    }
}
