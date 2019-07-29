using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Horizon.Common.Repository.Legacy.Models.Common;
using Solana.Web.Admin.Models.Requests.IntegrationMaps.NestedModels;

namespace Solana.Web.Admin.Models.Requests.IntegrationMaps
{
    public class PutIntegrationMapsRequest
    {
        public int TriageFileID { get; set; }
        public bool DeletePreviewFile { get; set; }
        public int AdmIntegrationMapID { get; set; }
        [Required(ErrorMessage = "Map Name is required.")]
        public string MapName { get; set; }
        public bool IsExport { get; set; }
        public bool IsImport { get; set; }
        public IntegrationMapType MapType { get; set; }
        public string MapTypeDescription { get; set; }
        [Required]
        public string AppObjectID { get; set; }
        public string FileLocation { get; set; }
        public IntegrationMapEndpoint FileLocationType { get; set; }
        public IntegrationMapFileType FileType { get; set; }
        public int FT { get; set; }
        [MaxLength(1)]
        public string FileUserDefinedDelimiter { get; set; }
        public DateTime? LastRun { get; set; }
        public int BeginOnLine { get; set; }
        public bool UseDoubleQuote { get; set; }
        public bool UseHeaderDetail { get; set; }
        public int MapDateFormat { get; set; }

        public IntegrationMapPreviewFileInfoSaveModel PreviewFileInfoSave { get; set; }

        public ICollection<IntegrationMapColumnSaveModel> Columns { get; set; }
        public ICollection<AvailableAppObjectSaveModel> AvailableAppObjects { get; set; }
        public ICollection<AvailableFieldSaveModel> AvailableFields { get; set; }
    }
}