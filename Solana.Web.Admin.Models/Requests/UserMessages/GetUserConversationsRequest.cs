namespace Solana.Web.Admin.Models.Requests.UserMessages
{
    public class GetUserConversationsRequest
    {
        public int AdmUserId { get; set; }
        public int MessageId { get; set; }
    }
}