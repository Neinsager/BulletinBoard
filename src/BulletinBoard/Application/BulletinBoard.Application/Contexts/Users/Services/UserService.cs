using AutoMapper;
using BulletinBoard.Application.AppServices.Contexts.Users.Repositories;
using BulletinBoard.Application.AppServices.Exceptions;
using BulletinBoard.Contracts.Users;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using BulletinBoard.Application.AppServices.Authentication.Constants;
using BulletinBoard.Application.AppServices.Authentication.EntitiesAuth;
using BulletinBoard.Application.AppServices.Authentication.Passwords;

namespace BulletinBoard.Application.AppServices.Contexts.Users.Services
{
    /// <inheritdoc cref="IUserService"/>
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;
        private readonly IMapper _mapper;
        private readonly IEntityAuthorizationService _entityAuthorizationService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IPasswordService _passwordService;

       /// <summary>
       /// Инициализирует экземпляр <see cref="UserService"/>
       /// </summary>
       /// <param name="userRepository">Репозиторий.</param>
       /// <param name="httpContextAccessor"></param>
       /// <param name="mapper">Маппер.</param>
       /// <param name="entityAuthorizationService"></param>
       public UserService(IUserRepository userRepository, IMapper mapper,
            IHttpContextAccessor httpContextAccessor,
            IEntityAuthorizationService entityAuthorizationService,
            IPasswordService passwordService)
        {
            _repository = userRepository;
            _mapper = mapper;
            _entityAuthorizationService = entityAuthorizationService;
            _httpContextAccessor = httpContextAccessor;
            _passwordService = passwordService;
        }

        /// <inheritdoc/>
        public Task<UserDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return _repository.GetByIdAsync(id, cancellationToken);
        }

        /// <inheritdoc/>
        public Task<UserInfoDto> GetCurrentUser(CancellationToken cancellationToken)
        {
            var userId = Guid.Parse(_httpContextAccessor.HttpContext!.User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            return _repository.GetCurrentUser(userId, cancellationToken);
        }

        /// <inheritdoc/>
        public Task UpdateAsync(Guid id, UpdateUserDto dto, CancellationToken cancellationToken)
        {
            return _repository.GetByPredicate(u => u.Id == id, cancellationToken).ContinueWith(t =>
            {
                var user = t.Result ?? throw new EntityNotFoundException();

                return _entityAuthorizationService.ValidateUserOnly(_httpContextAccessor.HttpContext!.User, id, AuthRoles.Admin).ContinueWith(t2 =>
                {
                    if (t2.Result)
                        throw new EntityForbiddenException();

                    user.Name = dto.Name;
                    user.PasswordHash = _passwordService.HashPassword(dto.Password);

                    return _repository.UpdateAsync(id, user, cancellationToken);
                }).Unwrap();
            }).Unwrap();
        }

        /// <inheritdoc/>
        public Task DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            return _repository.GetByPredicate(u => u.Id == id, cancellationToken).ContinueWith(t =>
            {
                var user = t.Result ?? throw new EntityNotFoundException();

                return _entityAuthorizationService.ValidateUserOnly(_httpContextAccessor.HttpContext!.User, id, AuthRoles.Admin).ContinueWith(t2 =>
                {
                    if (t2.Result) throw new EntityForbiddenException();
                    return _repository.DeleteAsync(user, cancellationToken);
                }).Unwrap();
            }).Unwrap();
    }

        /// <inheritdoc/>
        public async Task<List<UserInfoDto>> GetAllAsync(CancellationToken cancellationToken)
        {
            var listUsers = await _repository.GetAllAsync(cancellationToken);
            return listUsers;
        }
    }
}