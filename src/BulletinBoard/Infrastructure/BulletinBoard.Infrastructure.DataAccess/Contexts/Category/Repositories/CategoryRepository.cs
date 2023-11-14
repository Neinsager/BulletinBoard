using AutoMapper;
using BulletinBoard.Application.AppServices.Contexts.Categories.Repositories;
using BulletinBoard.Contracts.Categories;
using BulletinBoard.Domain.Categories;
using BulletinBoard.Infrastructure.Repository;
using System.Linq.Expressions;

namespace BulletinBoard.Infrastructure.DataAccess.Contexts.Categories.Repositories
{
    /// <inheritdoc cref="ICategoryRepository"/>
    public class CategoryRepository : ICategoryRepository
    {
        private readonly IRepository<Category> _repository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Инициализирует репозиторий <see cref="CategoryRepository"/>
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="mapper"></param>
        public CategoryRepository(IRepository<Category> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        /// <inheritdoc/>
        public Task<Guid> CreateAsync(Category category, CancellationToken cancellationToken)
        {
            _repository.AddAsync(category, cancellationToken);
            return Task.FromResult(category.Id);
        }

        /// <inheritdoc/>
        public Task DeleteByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var category = GetByIdAsync(id, cancellationToken);
            var mapCategore = _mapper.Map<Category>(category);
            return _repository.DeleteAsync(mapCategore, cancellationToken);
        }

        /// <inheritdoc/>
        public Task<IReadOnlyCollection<CategoryInfoDto>> GetAllAsync(CancellationToken cancellationToken)
        {
            var categoryCollection = _repository.GetAll();
            var dtoCollection = _mapper.Map<List<CategoryInfoDto>>(categoryCollection.ToList());
            IReadOnlyCollection<CategoryInfoDto> readonlyCollection = dtoCollection.AsReadOnly();

            return Task.Run(() => readonlyCollection);
        }

        /// <inheritdoc/>
        public Task<CategoryDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return _repository.GetByIdAsync(id, cancellationToken).ContinueWith(t =>
            {
                var category = t.Result;
                return _mapper.Map<CategoryDto?>(category);
            });
        }

        /// <inheritdoc/>
        public Task<Category> GetByPredicate(Expression<Func<Category, bool>> predicate, CancellationToken cancellationToken)
        {
            return Task.Run(() => (_repository.GetAllFiltered(predicate).FirstOrDefault()), cancellationToken);
        }

        /// <inheritdoc/>
        public Task UpdateAsync(Guid id, Category category, CancellationToken cancellationToken)
        {
            return _repository.UpdateAsync(category, cancellationToken);
        }
    }
}
