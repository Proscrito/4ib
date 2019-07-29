namespace Solana.Web.Admin.Models.Responses.SchoolGroups.NestedModels
{
    public class AdmSchoolGroupSiteSaveModel
    {
        public int AdmSiteID { get; set; }
        public string SchoolID { get; set; }
        public string SchoolName { get; set; }
        public bool IsSelected { get; set; }
        public bool IsCEPSchool { get; set; }
    }
}
