using AutoMapper;
using BulletinBoard.Domain.Posts;
using BulletinBoard.Contracts.Posts;
using System.Security.Principal;

namespace BulletinBoard.Infrastructure.MappingProfiles
{
    /// <summary>
    /// Маппер для <see cref="Post"/>.
    /// </summary>
    public class PostMapProfile : Profile
    {
        /// <summary>
        /// Маппинг для <see cref="Post"/>.
        /// </summary>
        public PostMapProfile()
        {
            CreateMap<Post, PostDto>(MemberList.None)
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price))
                .ForMember(dest => dest.User, opt => opt.MapFrom(src => src.User));

            CreateMap<Post, PostInfoDto>(MemberList.None)
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price));

            CreateMap<CreatePostDto, Post>(MemberList.None)
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price))
                .ForMember(dest => dest.CategoryId, opt => opt.MapFrom(src => src.CategoryId));

            CreateMap<UpdatePostDto, Post>(MemberList.None).IncludeBase<CreatePostDto, Post>();
        }
    }
}