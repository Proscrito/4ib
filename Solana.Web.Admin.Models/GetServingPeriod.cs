using Horizon.Common.Models.Enumerations;
using System;

namespace Solana.Web.Admin.Models
{
    public class GetServingPeriod
    {
        public int AdmServingPeriodID { get; set; }
        public string Description { get; set; }
        public int DisplayOrder { get; set; }
        public ServingPeriodTypes PeriodType { get; set; }
        public decimal? ConversionFactor { get; set; }
        public string Abbreviation { get; set; }
        public MenuAnalysisType? MenuAnalysisType { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDateTime { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTime? LastModifiedDateTime { get; set; }
    }
}
