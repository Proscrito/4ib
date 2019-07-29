namespace Solana.Web.Admin.Models.Requests.IntegrationMaps.NestedModels
{
    public class IntegrationMapPreviewFileInfoSaveModel
    {
        public int AdmIntegrationMapsFileID { get; set; }
        public string FileName { get; set; }
        public long SizeInBytes { get; set; }
    }
}