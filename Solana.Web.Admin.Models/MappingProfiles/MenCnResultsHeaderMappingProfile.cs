using AutoMapper;
using Horizon.Common.Repository.Legacy.Models.Men;
using Solana.Web.Admin.Models.Responses.CNImport.NestedModels;

namespace Solana.Web.Admin.Models.MappingProfiles
{
    public class MenCnResultsHeaderMappingProfile : Profile
    {
        public MenCnResultsHeaderMappingProfile()
        {
            CreateMap<MenCnResultsHeader, MenCnResultsHeaderModel>()
                .ForMember(d => d.UserName, o =>
                {
                    o.Condition(s => s.AdmUser != null);
                    o.MapFrom(s => s.AdmUser.UserLogin);
                });
        }
    }
}
