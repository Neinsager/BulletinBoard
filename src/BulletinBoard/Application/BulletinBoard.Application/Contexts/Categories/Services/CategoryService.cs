using AutoMapper;
using BulletinBoard.Application.AppServices.Contexts.Categories.Repositories;
using BulletinBoard.Application.AppServices.Exceptions;
using BulletinBoard.Contracts.Categories;
using BulletinBoard.Domain.Categories;

namespace BulletinBoard.Application.AppServices.Contexts.Categories.Services
{
    /// <inheritdoc />
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _repository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Инициализирует экземпляр <see cref="CategoryService"/>.
        /// </summary>
        /// <param name="repository">Репозиторий для работы с категориями.</param>
        /// <param name="mapper">Маппер.</param>
        public CategoryService(ICategoryRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        /// <inheritdoc />
        public async Task<CategoryInfoDto> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var subCategory = await _repository.GetByIdAsync(id, cancellationToken);
            var dto = _mapper.Map<CategoryInfoDto>(subCategory);
            return dto;
        }

        /// <inheritdoc />
        public async Task<IReadOnlyCollection<CategoryInfoDto>> GetAllAsync(CancellationToken cancellationToken)
        {
            var listCategories = await _repository.GetAllAsync(cancellationToken);
            return listCategories;
        }

        /// <inheritdoc />
        public Task<Guid> CreateAsync(CreateCategoryDto dto, CancellationToken cancellationToken)
        {
            var category = _mapper.Map<Category>(dto);
            return _repository.CreateAsync(category, cancellationToken);
        }

        /// <inheritdoc />
        public Task UpdateByIdAsync(Guid id, UpdateCategoryDto dto,
            CancellationToken cancellationToken)
        {
            return _repository.GetByPredicate(c => c.Id == id, cancellationToken).ContinueWith(t =>
            {
                var category = t.Result ?? throw new EntityNotFoundException();
                category.CategoryName = dto.CategoryName;
                category.Id = dto.ParentId;
                return _repository.UpdateAsync(id, category, cancellationToken);
            }).Unwrap();
        }

        /// <inheritdoc />
        public async Task DeleteByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            await _repository.DeleteByIdAsync(id, cancellationToken);
        }
    }
}