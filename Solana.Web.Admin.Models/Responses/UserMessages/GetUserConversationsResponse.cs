using System.Collections.Generic;
using Solana.Web.Admin.Models.Responses.UserMessages.NestedModels;

namespace Solana.Web.Admin.Models.Responses.UserMessages
{
    public class GetUserConversationsResponse
    {
        public ICollection<ConversationModel> Conversations { get; set; }
    }
}