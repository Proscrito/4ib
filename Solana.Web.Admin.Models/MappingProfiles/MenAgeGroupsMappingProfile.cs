using System.Linq;
using AutoMapper;
using Horizon.Common.Repository.Legacy.Models.Men;
using Solana.Web.Admin.Models.Responses.GlobalOptions.NestedModels;

namespace Solana.Web.Admin.Models.MappingProfiles
{
    public class MenAgeGroupsMappingProfile : Profile
    {
        public MenAgeGroupsMappingProfile()
        {
            CreateMap<MenAgeGroups, MenAgeGroupsModel>()
                .ForMember(dest => dest.IsAssigned, opt => opt.MapFrom(x => x.AdmSites.Any()));
        }
    }
}
