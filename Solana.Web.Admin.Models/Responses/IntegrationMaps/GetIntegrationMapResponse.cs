using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Horizon.Common.Repository.Legacy.Models.Common;
using Solana.Web.Admin.Models.Responses.IntegrationMaps.NestedModels;

namespace Solana.Web.Admin.Models.Responses.IntegrationMaps
{
    public class GetIntegrationMapResponse
    {
        public int TriageFileID { get; set; }
        public bool DeletePreviewFile { get; set; }
        public int AdmIntegrationMapID { get; set; }
        public string MapName { get; set; }
        public bool IsExport { get; set; }
        public bool IsImport { get; set; }
        public IntegrationMapType MapType { get; set; }
        public string MapTypeDescription { get; set; }
        public string AppObjectID { get; set; }
        public string FileLocation { get; set; }
        public IntegrationMapEndpoint FileLocationType { get; set; }
        public IntegrationMapFileType FileType { get; set; }
        public int FT { get; set; }
        public string FileUserDefinedDelimiter { get; set; }
        public DateTime? LastRun { get; set; }
        public int BeginOnLine { get; set; }
        public bool UseDoubleQuote { get; set; }
        public bool UseHeaderDetail { get; set; }
        public int MapDateFormat { get; set; }

        public IntegrationMapPreviewFileInfoViewModel PreviewFileInfo { get; set; }
        
        public ICollection<IntegrationMapColumnViewModel> Columns { get; set; }
        public ICollection<AvailableAppObjectViewModel> AvailableAppObjects { get; set; }
        public ICollection<AvailableFieldViewModel> AvailableFields { get; set; }
    }
}
