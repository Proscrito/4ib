using AutoMapper;
using Horizon.Common.Repository.Legacy.Models.Adm;
using Horizon.Common.Repository.Legacy.Models.Far;
using Solana.Web.Admin.Models.Const;
using Solana.Web.Admin.Models.Responses.GlobalOptions;

namespace Solana.Web.Admin.Models.MappingProfiles
{
    public class AdmGlobalOptionMappingProfile : Profile
    {
        public AdmGlobalOptionMappingProfile()
        {
            CreateMap<AdmGlobalOption, GetAdmGlobalOptionsResponse>()
                .ForMember(dest => dest.NumberOfUnsuccessfulLoginAttemptsBeforeLockout, opt => opt.MapFrom(x => AdmGlobalOptionConst.LoginUnsuccessfulAttempts))
                .ForMember(dest => dest.InactivityMinutesForLogoutTerminal, opt => opt.MapFrom(x => AdmGlobalOptionConst.TerminalAutoLogout))
                .ForMember(dest => dest.InactivityMinutesForLogoutWeb, opt => opt.MapFrom(x => AdmGlobalOptionConst.WebAutoLogout));

            CreateMap<FarOption, GetAdmGlobalOptionsResponse>()
                .ForMember(dest => dest.UseCalendarDays, opt => opt.MapFrom(x => x.UseCalendarDays ? 1 : 2));
        }
    }
}
