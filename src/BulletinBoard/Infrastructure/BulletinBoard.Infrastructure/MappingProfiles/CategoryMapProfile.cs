using AutoMapper;
using BulletinBoard.Contracts.Categories;
using BulletinBoard.Domain.Categories;

namespace BulletinBoard.Infrastructure.MappingProfiles
{
    /// <summary>
    /// Маппер для <see cref="Category"/>.
    /// </summary>
    public class CategoryMapProfile : Profile
    {
        /// <summary>
        /// Маппинг для <see cref="Category"/>.
        /// </summary>
        public CategoryMapProfile()
        {
            CreateMap<Category, CategoryDto>(MemberList.None)
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.CategoryName))
                .ForMember(dest => dest.SubCategories, opt => opt.MapFrom(src => src.SubCategories))
                .ForMember(dest => dest.ParentId, opt => opt.MapFrom(src => src.ParentId))
                .ForMember(dest => dest.Posts, opt => opt.MapFrom(src => src.Posts));

            CreateMap<Category, CategoryInfoDto>(MemberList.None)
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.CategoryName))
                .ForMember(dest => dest.ParentId, opt => opt.MapFrom(src => src.ParentId));

            CreateMap<CreateCategoryDto, Category> (MemberList.None)
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.CategoryName))
                .ForMember(dest => dest.ParentId, opt => opt.MapFrom(src => src.ParentId));

            CreateMap<CategoryDto, CategoryInfoDto>(MemberList.None)
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.CategoryName))
            .ForMember(dest => dest.ParentId, opt => opt.MapFrom(src => src.ParentId));

            CreateMap<UpdateCategoryDto , Category>(MemberList.None).IncludeBase<CreateCategoryDto, Category>();
        }
    }
}
