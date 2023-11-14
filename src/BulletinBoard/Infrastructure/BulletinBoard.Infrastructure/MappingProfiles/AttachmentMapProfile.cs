using AutoMapper;
using BulletinBoard.Contracts.Attachments;
using BulletinBoard.Domain.Attachments;
using BulletinBoard.Application.AppServices.Files;

namespace BulletinBoard.Infrastructure.MappingProfiles
{
    /// <summary>
    /// Маппер для <see cref="Attachment"/>.
    /// </summary>
    public class AttachmentMapProfile : Profile
    {
        /// <summary>
        /// Маппинг для <see cref="Attachment"/>.
        /// </summary>
        public AttachmentMapProfile()
        {
            CreateMap<Attachment, AttachmentDto>(MemberList.None)
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Post, opt => opt.MapFrom(src => src.Post))
                .ForMember(dest => dest.Content, opt => opt.MapFrom(src => src.Content));
                

            CreateMap<CreateAttachmentDto, Attachment>(MemberList.None)
                .ForMember(dest => dest.PostId, opt => opt.MapFrom(src => src.PostId))
                .ForMember(dest => dest.Content, opt => opt.MapFrom(src => FileToBytes.ProcessAsync(src.File).Result));
        }
    }
}