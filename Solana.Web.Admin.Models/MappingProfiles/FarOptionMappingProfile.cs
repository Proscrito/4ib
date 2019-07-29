using AutoMapper;
using Horizon.Common.Repository.Legacy.Models.Far;
using Solana.Web.Admin.Models.Requests.GlobalOptions;

namespace Solana.Web.Admin.Models.MappingProfiles
{
    public class FarOptionMappingProfile : Profile
    {
        public FarOptionMappingProfile()
        {
            CreateMap<PutAdmGlobalOptionsRequest, FarOption>()
                .ForMember(dest => dest.UseCalendarDays, opt => opt.MapFrom(x => x.UseCalendarDays == 1));
        }
    }
}
