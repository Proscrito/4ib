using System;

namespace Solana.Web.Admin.Models.Responses.YearEndProcess.NestedModels
{
    public class SavedSitePromotionModel
    {
        public int AdmSiteID { get; set; }
        public int PosGradeID { get; set; }
        public int? PromoteToAdmSiteID { get; set; }
        public int LastUpdatedBy { get; set; }
        public DateTime LastUpdatedDate { get; set; }
    }
}
