using System.Collections.Generic;
using Horizon.Common.Repository.Legacy.Models.Common;

namespace Solana.Web.Admin.Models.Requests.IntegrationMaps.NestedModels
{
    public class IntegrationMapColumnSaveModel
    {
        public int AdmIntegrationMapsColumnID { get; set; }
        public int AdmIntegrationMapID { get; set; }
        public int Position { get; set; }
        public string VisiblePosition { get; set; }
        public int? AdmIntegrationMapsAvailableFieldID { get; set; }
        public string FieldDescription { get; set; }
        public int Length { get; set; }
        public int MaximumLength { get; set; }
        public string PadCharacter { get; set; }
        public int TotalPadLength { get; set; }

        public bool IsUsedForMatching { get; set; }
        public IntegrationMapColumnPadding ColumnPadding { get; set; }
        public bool IsUsedForSecondaryMatching { get; set; }
        public bool IsUsedForAppMatching { get; set; }
        public bool IsTranslatable { get; set; }
        public string ConstantValue { get; set; }
        public bool IsHeader { get; set; }
        public List<IntegrationMapColumnTranslationSaveModel> Translations { get; set; }
    }
}