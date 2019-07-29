namespace Solana.Web.Admin.Models.Requests.IntegrationMaps
{
    public class PutAppTriageFileRequest
    {
        public int AppTriageFileID { get; set; }
        public System.DateTime UploadDate { get; set; }
        public string FileName { get; set; }
        public long Size { get; set; }
        public byte[] Data { get; set; }
    }
}
