using System;

namespace Solana.Web.Admin.Models.Responses.UserMessages.NestedModels
{
    public class UserMessage
    {
        public int AdmMessageId { get; set; }
        public string UserName { get; set; }
        public string SiteDescription { get; set; }
        public string GroupName { get; set; }
        public DateTime MessageDate { get; set; }
        public bool IsRead { get; set; }
        public bool IsUrgent { get; set; }
        public int AdmUserId { get; set; }
    }
}
