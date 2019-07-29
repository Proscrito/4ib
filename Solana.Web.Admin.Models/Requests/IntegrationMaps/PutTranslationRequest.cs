namespace Solana.Web.Admin.Models.Requests.IntegrationMaps
{
    public class PutTranslationRequest
    {
        public int AdmIntegrationMapsColumnsTranslationID { get; set; }
        public int AdmIntegrationMapsColumnID { get; set; }
        public string OriginalValue { get; set; }
        public string TranslatedValue { get; set; }
    }
}
