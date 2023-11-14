using BulletinBoard.Application.AppServices.Authentication.EntitiesAuth;
using BulletinBoard.Application.AppServices.Contexts.Posts.Repositories;
using System.Security.Claims;

namespace BulletinBoard.Application.AppServices.Authentication.Services
{
    /// <inheritdoc cref="IEntityAuthorizationService"/>
    public class EntityAuthorizationService : IEntityAuthorizationService
    {
        private readonly IPostRepository _repository;

        /// <summary>
        /// Инициализация сервиса проверки прав доступа.
        /// </summary>
        /// <param name="adRepository"></param>
        public EntityAuthorizationService(IPostRepository repository)
        {
            _repository = repository;
        }

        /// <inheritdoc/>
        public Task<bool> Validate(ClaimsPrincipal user, Guid entityId, string role)
        {
            return _repository.GetByIdAsync(entityId, CancellationToken.None).ContinueWith(t =>
            {
                var entity = t.Result;
                var userId = user.FindFirstValue(ClaimTypes.NameIdentifier);
                var userRole = user.FindFirstValue(ClaimTypes.Role);

                return entity!.User.Id.ToString() != userId && userRole != role;
            });
        }

        /// <inheritdoc/>
        public Task<bool> ValidateUserOnly(ClaimsPrincipal user, Guid userId, string role)
        {
            var currentUserId = user.FindFirstValue(ClaimTypes.NameIdentifier);
            var currentUserRole = user.FindFirstValue(ClaimTypes.Role);

            return Task.FromResult(currentUserId != userId.ToString() && currentUserRole != role);
        }
    }
}