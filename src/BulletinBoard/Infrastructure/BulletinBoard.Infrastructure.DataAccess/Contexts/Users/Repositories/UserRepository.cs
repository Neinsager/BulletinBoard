using AutoMapper;
using AutoMapper.QueryableExtensions;
using BulletinBoard.Application.AppServices.Contexts.Users.Repositories;
using BulletinBoard.Contracts.Users;
using BulletinBoard.Domain.Users;
using BulletinBoard.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BulletinBoard.Infrastructure.DataAccess.Contexts.Users.Repositories
{
    /// <inheritdoc cref="IUserRepository"/>
    public class UserRepository : IUserRepository
    {
        private readonly IRepository<User> _repository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Инициализирует репозиторий пользователей.
        /// </summary>
        /// <param name="repository">Базовый репозиторий.</param>
        /// <param name="mapper">Маппер.</param>
        public UserRepository(IRepository<User> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        /// <inheritdoc/>
        public Task<UserDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return _repository.GetByIdAsync(id, cancellationToken).ContinueWith(t =>
            {
                var user = t.Result;
                return _mapper.Map<UserDto?>(user);
            });
        }

        /// <inheritdoc/>
        public Task<UserInfoDto> GetCurrentUser(Guid id, CancellationToken cancellationToken)
        {
            return _repository.GetByIdAsync(id, cancellationToken).ContinueWith(t =>
            {
                var user = t.Result;
                return _mapper.Map<UserInfoDto>(user);
            });
        }

        /// <inheritdoc/>
        public Task<User?> GetByPredicate(Expression<Func<User, bool>> predicate, CancellationToken cancellationToken)
        {
            return Task.Run(() => (_repository.GetAllFiltered(predicate).FirstOrDefault()), cancellationToken);
        }

        /// <inheritdoc/>
        public Task<Guid> CreateAsync(User user, CancellationToken cancellationToken)
        {
            _repository.AddAsync(user, cancellationToken);
            return Task.FromResult(user.Id);
        }

        /// <inheritdoc/>
        public Task UpdateAsync(Guid id, User user, CancellationToken cancellationToken)
        {
            return _repository.UpdateAsync(user, cancellationToken);
        }

        /// <inheritdoc/>
        public Task DeleteAsync(User user, CancellationToken cancellationToken)
        {
            return _repository.DeleteAsync(user, cancellationToken);
        }

        /// <inheritdoc />
        public async Task<List<UserInfoDto>> GetAllAsync(CancellationToken cancellationToken)
        {
            var listUsers = await _repository.GetAll()
                .AsNoTracking()
                .ProjectTo<UserInfoDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);
            return listUsers;
        }
    }
}