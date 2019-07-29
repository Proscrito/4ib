namespace Solana.Web.Admin.Models.Requests.GlobalOptions.NestedModels
{
    public class MenLeftoverCodeSaveModel
    {
        public int MenLeftoverCodeID { get; set; }
        public string Description { get; set; }
        public string Code { get; set; }
        public int MenLeftoverReasonID { get; set; }
        public bool Preloaded { get; set; }
        public bool IsDeleted { get; set; }
    }
}
