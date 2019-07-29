using System.Collections.Generic;

namespace Solana.Web.Admin.Models.Requests.UserMessages
{
    public class PostMessageUpdateRequest
    {
        public string Message { get; set; }
        public int AdmUserId { get; set; }
        public int AdmSiteId { get; set; } 
        public bool IsUrgent { get; set; }
        public ICollection<int> UserIDs { get; set; }
        public string ActionType { get; set; }
    }
}