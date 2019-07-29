namespace Solana.Web.Admin.Models.Requests.GlobalOptions.NestedModels
{
    public class InvTransactionTypeSaveModel
    {
        public int InvTransactionTypeID { get; set; }
        public string Description { get; set; }
        public bool Active { get; set; }
        public string Code { get; set; }
        public int? Operation { get; set; }
        public int DisplayOrder { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsUserDefined { get; set; }
    }
}
