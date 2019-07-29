namespace Solana.Web.Admin.Models.Requests.UserMessages
{
    public class PutReplyUpdateRequest
    { 
        public int AdmMessageId { get; set; }
        public int AdmUserId { get; set; }
        public int AdmSiteId { get; set; } 
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string SiteName { get; set; }
        public string GroupName { get; set; }
        public string Message { get; set; }
    }
}