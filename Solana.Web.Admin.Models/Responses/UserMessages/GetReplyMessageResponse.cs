namespace Solana.Web.Admin.Models.Responses.UserMessages
{
    public class GetReplyMessageResponse
    {
        public int AdmMessageId { get; set; }
        public string UserIDs { get; set; }
        public string UserNames { get; set; }
        public string Message { get; set; }
    }
}