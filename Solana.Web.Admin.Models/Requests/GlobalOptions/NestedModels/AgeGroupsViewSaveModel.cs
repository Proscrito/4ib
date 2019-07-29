namespace Solana.Web.Admin.Models.Requests.GlobalOptions.NestedModels
{
    public class AgeGroupsViewSaveModel
    {
        public int MenAgeGroupID { get; set; }

        public string Description { get; set; }

        public bool IsActive { get; set; }

        public bool IsAssigned { get; set; }
    }
}
