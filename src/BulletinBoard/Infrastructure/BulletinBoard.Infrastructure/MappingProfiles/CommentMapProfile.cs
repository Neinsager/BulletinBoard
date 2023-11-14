using AutoMapper;
using BulletinBoard.Contracts.Comments;
using BulletinBoard.Domain.Comments;

namespace BulletinBoard.Infrastructure.MappingProfiles
{
    /// <summary>
    /// Маппер для <see cref="Comment"/>.
    /// </summary>
    public class CommentMapProfile : Profile
    {
        /// <summary>
        /// Маппинг для <see cref="Comment"/>.
        /// </summary>
        public CommentMapProfile()
        {
            CreateMap<Comment, CommentDto>(MemberList.None)
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Content, opt => opt.MapFrom(src => src.Content))
                .ForMember(dest => dest.PostDto, opt => opt.MapFrom(src => src.Post))
                .ForMember(dest => dest.PostId, opt => opt.MapFrom(src => src.PostId))
                .ForMember(dest => dest.User, opt => opt.MapFrom(src => src.User))
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId));

            CreateMap<Comment, CommentInfoDto>(MemberList.None)
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Content, opt => opt.MapFrom(src => src.Content))
                .ForMember(dest => dest.PostId, opt => opt.MapFrom(src => src.PostId));

            CreateMap<CreateCommentDto, Comment>(MemberList.None)
                .ForMember(dest => dest.PostId, opt => opt.MapFrom(src => src.PostId))
                .ForMember(dest => dest.Content, opt => opt.MapFrom(src => src.Content));

            CreateMap<UpdateCommentDto , Comment>(MemberList.None)
                .ForMember(dest => dest.Content, opt => opt.MapFrom(src => src.Content));
        }
    }
}
