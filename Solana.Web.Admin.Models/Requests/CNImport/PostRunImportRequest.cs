namespace Solana.Web.Admin.Models.Requests.CNImport
{
    public class PostRunImportRequest
    {
        public string AvailableCN { get; set; }
        public int AdmUserID { get; set; }
        public int CustomerID { get; set; }
        //TODO: there is a question how to build those path, for now let's expect them as parameters
        public string ZipPath { get; set; }
        public string TempPath { get; set; }
    }
}
