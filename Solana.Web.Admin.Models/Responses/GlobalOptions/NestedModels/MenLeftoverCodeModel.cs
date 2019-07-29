namespace Solana.Web.Admin.Models.Responses.GlobalOptions.NestedModels
{
    public class MenLeftoverCodeModel
    {
        public int MenLeftoverCodeID { get; set; }
        public string Description { get; set; }
        public string Code { get; set; }
        public int MenLeftoverReasonID { get; set; }
        public bool Preloaded { get; set; }
        public bool IsDeleted { get; set; }
    }
}
