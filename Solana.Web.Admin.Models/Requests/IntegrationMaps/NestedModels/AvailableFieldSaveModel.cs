namespace Solana.Web.Admin.Models.Requests.IntegrationMaps.NestedModels
{
    public class AvailableFieldSaveModel
    {
        public int AdmIntegrationMapsAvailableFieldID { get; set; }
        public string FieldDescription { get; set; }
        public bool IsTranslatable { get; set; }
        public string ForeignColumnKey { get; set; }
        public bool IsBoolean { get; set; }
    }
}