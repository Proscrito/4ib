using System;
using AutoMapper;
using Horizon.Common.Repository.Legacy.Models.Adm;
using Horizon.Common.Repository.Legacy.Models.App;
using Horizon.Common.Repository.Legacy.Models.Common;
using Solana.Web.Admin.Models.Requests.IntegrationMaps.NestedModels;
using Solana.Web.Admin.Models.Responses.IntegrationMaps;
using Solana.Web.Admin.Models.Responses.IntegrationMaps.NestedModels;

namespace Solana.Web.Admin.Models.MappingProfiles
{
    public class IntegrationMapMappingProfile : Profile
    {
        public IntegrationMapMappingProfile()
        {
            CreateMap<AdmIntegrationMap, GetIntegrationMapResponse>()
                .ForMember(r => r.MapTypeDescription, e => e.MapFrom(s => ((IntegrationMapType)s.MapType).GetDescription()))
                .ForMember(r => r.FT, e => e.MapFrom(s => (int)s.FileType))
                .ForMember(r => r.Columns, e => e.MapFrom(s => s.AdmIntegrationMapsColumns))
                .ForMember(r => r.AvailableFields, e => e.MapFrom(s => s.AppObject.AdmIntegrationMapsAvailableFields))
                .ForMember(r => r.PreviewFileInfo, e => e.MapFrom(s => s.PreviewFile));

            CreateMap<AdmIntegrationMap, IntegrationMapViewModel>()
                .ForMember(r => r.MapTypeName, e => e.MapFrom(s => Enum.GetName(typeof(IntegrationMapType), s.MapType)))
                .ForMember(r => r.MapTypeDescription, e => e.MapFrom(s => ((IntegrationMapType)s.MapType).GetDescription()))
                .ForMember(r => r.JobType, e => e.MapFrom(s => s.IsExport ? "Import" : "Export"));

            CreateMap<AdmIntegrationMapsPreviewFile, IntegrationMapPreviewFileInfoViewModel>()
                .ForMember(r => r.SizeInBytes, e => e.MapFrom(s => s.Size));

            CreateMap<AppObject, AvailableAppObjectViewModel>()
                .ForMember(r => r.Text, e => e.MapFrom(s => s.AppObjectID))
                .ForMember(r => r.Value, e => e.MapFrom(s => s.AppObjectID));

            CreateMap<IntegrationMapColumnSaveModel, AdmIntegrationMapsColumn>()
                .ForMember(r => r.AdmIntegrationMapsColumnsTranslations, e => e.MapFrom(s => s.Translations));

            CreateMap<IntegrationMapColumnTranslationSaveModel, AdmIntegrationMapsColumnsTranslation>()
                .ForMember(r => r.TranslatedValue, e => e.MapFrom(s => YesNoToIntString(s.TranslatedValue)));
        }

        //TODO: it's something weird. All conversion between "Yes", "No", "Maybe" etc and bool should happen on UI side
        private static string YesNoToIntString(string val)
        {
            return val.Equals("Yes", StringComparison.CurrentCultureIgnoreCase)
                ? "1"
                : val.Equals("No", StringComparison.CurrentCultureIgnoreCase)
                    ? "0"
                    : val;
        }
    }
}
