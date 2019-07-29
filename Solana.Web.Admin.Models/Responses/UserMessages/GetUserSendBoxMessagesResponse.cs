using System.Collections.Generic;
using Solana.Web.Admin.Models.Responses.UserMessages.NestedModels;

namespace Solana.Web.Admin.Models.Responses.UserMessages
{
    public class GetUserSendBoxMessagesResponse
    {
        public ICollection<UserMessage> SentMessages { get; set; }
    }
}
