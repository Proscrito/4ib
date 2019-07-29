using System.Threading.Tasks;
using Solana.Web.Admin.Models.Requests.UserMessages;
using Solana.Web.Admin.Models.Responses.UserMessages;

namespace Solana.Web.Admin.BLL.Interfaces
{
    public interface IUserMessagesLogic
    {
        Task<GetUserSendBoxMessagesResponse> GetUserSendBoxMessages(int admUserId);
        Task<GetUserConversationsResponse> GetUserConversations(GetUserConversationsRequest request);
        Task<GetUserInboxBoxMessagesResponse> GetUserInboxBoxMessages(int admUserId);
        Task<GetReplyMessageResponse> ReplyMessage(int admMessageId);
        Task<GetShowUsersResponse> GetShowUsers();
        Task<bool> SendMessageUpdate(PostMessageUpdateRequest request);
        Task<bool> SendReplyUpdate(PutReplyUpdateRequest request);
        Task<bool> SetIsRead(int admMessageId);
    }
}
