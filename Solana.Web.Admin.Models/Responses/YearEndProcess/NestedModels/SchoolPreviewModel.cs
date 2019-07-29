namespace Solana.Web.Admin.Models.Responses.YearEndProcess.NestedModels
{
    public class SchoolPreviewModel
    {
        public int AdmSiteID { get; set; }
        public string SiteID { get; set; }
        public string SiteDescription { get; set; }
        public decimal Current_Liability { get; set; }
        public int Current_Student_Count { get; set; }
        public int Current_Adult_Count { get; set; }
        public int After_Student_Count { get; set; }
        public decimal After_Liability { get; set; }
        public int After_Adult_Count { get; set; }
    }
}
