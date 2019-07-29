namespace Solana.Web.Admin.Models.Responses.IntegrationMaps.NestedModels
{
    public class IntegrationMapColumnTranslationViewModel
    {
        public int AdmIntegrationMapsColumnsTranslationID { get; set; }
        public int AdmIntegrationMapsColumnID { get; set; }
        public string OriginalValue { get; set; }
        public string TranslatedValue { get; set; }
    }
}