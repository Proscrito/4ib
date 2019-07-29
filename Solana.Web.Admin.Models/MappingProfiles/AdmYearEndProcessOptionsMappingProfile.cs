using AutoMapper;
using Horizon.Common.Repository.Legacy.Models.Adm;
using Solana.Web.Admin.Models.Responses.YearEndProcess;

namespace Solana.Web.Admin.Models.MappingProfiles
{
    public class AdmYearEndProcessOptionsMappingProfile : Profile
    {
        public AdmYearEndProcessOptionsMappingProfile()
        {
            CreateMap<AdmYearEndProcessOptions, GetYearEndProcessSetUpOptionsResponse>()
                .ForMember(dest => dest.DefaultStartAltDate, opt => opt.MapFrom(src => src.DefaultStartAltDate.ToString()))
                .ForMember(dest => dest.DefaultStartDate, opt => opt.MapFrom(src => src.DefaultStartDate.ToString()))
                .ForMember(dest => dest.DefaultEndDate, opt => opt.MapFrom(src => src.DefaultEndDate.ToString()))
                .ForMember(dest => dest.DefaultTempStatusExpDate, opt => opt.MapFrom(src => src.DefaultTempStatusExpDate.ToString()))
                .ForMember(dest => dest.RolloverExecuted, opt => opt.MapFrom(src => src.RolloverExecuted.ToString()))
                .ForMember(dest => dest.RolloverStart, opt => opt.MapFrom(src => src.RolloverStart.ToString()));
        }
    }
}
