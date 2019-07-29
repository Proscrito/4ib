namespace Solana.Web.Admin.Models.Requests.ManagementLevels.NestedModels
{
    public class AdmManagementLevelSaveModel
    {
        public int AdmManagementLevelID { get; set; }

        public string Description { get; set; }

        public int ManagementLevelOrder { get; set; }
    }
}
