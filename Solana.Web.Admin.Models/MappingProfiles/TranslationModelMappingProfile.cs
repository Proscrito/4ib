using AutoMapper;
using Horizon.Common.Repository.Legacy.Models.SProcs;
using Solana.Web.Admin.Models.Responses.IntegrationMaps.NestedModels;

namespace Solana.Web.Admin.Models.MappingProfiles
{
    public class TranslationModelMappingProfile : Profile
    {
        public TranslationModelMappingProfile()
        {
            CreateMap<TranslatableValue, TranslationViewModel>()
                .ForMember(m => m.RowId, e => e.MapFrom(v => v.ROW_ID));
        }
    }
}
