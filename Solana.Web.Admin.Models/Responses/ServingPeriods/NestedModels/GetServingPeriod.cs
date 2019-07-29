using System.Collections.Generic;
using MenuAnalysisType = Horizon.Common.Repository.Legacy.Models.Common.MenuAnalysisType;
using ServingPeriodTypes = Horizon.Common.Repository.Legacy.Models.Common.ServingPeriodTypes;

namespace Solana.Web.Admin.Models.Responses.ServingPeriods.NestedModels
{
    public class ServingPeriod
    {
        public int AdmServingPeriodId { get; set; }
        public string Description { get; set; }
        public int DisplayOrder { get; set; }
        public ServingPeriodTypes PeriodType { get; set; }
        public decimal? ConversionFactor { get; set; }
        public string Abbreviation { get; set; }
        public MenuAnalysisType? MenuAnalysisType { get; set; }
        public ICollection<int> AssignedSiteIDs { get; set; }
    }
}
