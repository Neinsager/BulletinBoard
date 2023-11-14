using AutoMapper;
using BulletinBoard.Contracts.Users;
using BulletinBoard.Domain.Users;

namespace BulletinBoard.Infrastructure.MappingProfiles
{
    /// <summary>
    /// Маппер для <see cref="User"/>.
    /// </summary>
    public class UserMapProfile : Profile
    {
        /// <summary>
        /// Маппинг для <see cref="User"/>.
        /// </summary>
        public UserMapProfile()
        {
            CreateMap<CreateUserDto, User>(MemberList.None)
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Login, opt => opt.MapFrom(src => src.Login))
                .ForMember(dest => dest.PasswordHash, opt => opt.MapFrom(src => src.Password))
                .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email));
            

            CreateMap<User, UserDto>(MemberList.None)
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Login, opt => opt.MapFrom(src => src.Login))
                .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Role))
                .ForMember(dest => dest.Posts, opt => opt.MapFrom(src => src.Posts))
                .ForMember(dest => dest.Comments, opt => opt.MapFrom(src => src.Comments)).ReverseMap();

            CreateMap<User, UserInfoDto>(MemberList.None)
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Role))
                .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.PhoneNumber));

            CreateMap<UpdateUserDto, User>(MemberList.None).IncludeBase<CreateUserDto, User>();
        }
    }
}
