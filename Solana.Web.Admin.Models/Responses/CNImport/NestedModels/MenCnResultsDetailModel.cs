namespace Solana.Web.Admin.Models.Responses.CNImport.NestedModels
{
    public class MenCnResultsDetailModel
    {
        public int MenCnResultsDetailID { get; set; }
        public int MenCnResultsHeaderID { get; set; }
        public string TableName { get; set; }
        public int Imported { get; set; }
        public int Updated { get; set; }
        public int Discontinued { get; set; }
    }
}
