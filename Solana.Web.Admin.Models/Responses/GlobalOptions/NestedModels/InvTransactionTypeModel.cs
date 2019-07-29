namespace Solana.Web.Admin.Models.Responses.GlobalOptions.NestedModels
{
    public class InvTransactionTypeModel
    {
        public int InvTransactionTypeID { get; set; }
        public string Code { get; set; }
        public int? Operation { get; set; }
        public string Description { get; set; }
        public bool Preloaded { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsUserDefined { get; set; }
    }
}
