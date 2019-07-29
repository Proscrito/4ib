using AutoMapper;
using Horizon.Common.Repository.Legacy.Models.App;
using Solana.Web.Admin.Models.Requests.IntegrationMaps;

namespace Solana.Web.Admin.Models.MappingProfiles
{
    public class AppTriageFileMappingProfile : Profile
    {
        public AppTriageFileMappingProfile()
        {
            CreateMap<PutAppTriageFileRequest, AppTriageFile>()
                .ForPath(f => f.AppTriageFilesData.Data, e => e.MapFrom(r => r.Data));
        }
    }
}
