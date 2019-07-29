using System;

namespace Solana.Web.Admin.Models.Responses.CNImport.NestedModels
{
    public class MenCnResultsHeaderModel
    {
        public int MenCnResultsHeaderID { get; set; }
        public string CNVersionFrom { get; set; }
        public string CNVersionTo { get; set; }
        public int AdmUserID { get; set; }
        public string UserName { get; set; }
        public DateTime RunDate { get; set; }
        public DateTime CompleteDate { get; set; }
        public bool Result { get; set; }
        public string FailMessage { get; set; }
    }
}
