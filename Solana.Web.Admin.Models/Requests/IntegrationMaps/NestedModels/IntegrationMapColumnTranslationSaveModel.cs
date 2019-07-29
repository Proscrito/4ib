using System.ComponentModel.DataAnnotations;

namespace Solana.Web.Admin.Models.Requests.IntegrationMaps.NestedModels
{
    public class IntegrationMapColumnTranslationSaveModel
    {
        public int AdmIntegrationMapsColumnsTranslationID { get; set; }
        public int AdmIntegrationMapsColumnID { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string OriginalValue { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string TranslatedValue { get; set; }
    }
}