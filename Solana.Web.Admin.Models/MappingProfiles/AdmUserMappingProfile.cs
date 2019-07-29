using AutoMapper;
using Horizon.Common.Repository.Legacy.Models.Adm;
using Solana.Web.Admin.Models.Responses;

namespace Solana.Web.Admin.Models.MappingProfiles
{
    public class AdmUserMappingProfile : Profile
    {
        public AdmUserMappingProfile()
        {
            CreateMap<AdmUser, GetAdmUserResponse>()
                .ForMember(dest => dest.HasAdmUserGroup, opt => opt.MapFrom(src => src.AdmUserGroup != null));
        }
    }
}
