using System;

namespace Solana.Web.Admin.Models.Responses.UserMessages.NestedModels
{
    public class ConversationModel
    {
        public int AdmMessagesDetailID { get; set; }
        public int AdmMessageID { get; set; }
        public string UserName { get; set; }
        public string MesageText { get; set; }
        public bool IsRead { get; set; }
        public bool IsForwarded { get; set; }
        public DateTime MessageDate { get; set; }
    }
}
