using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Solana.Web.Admin.BLL.Interfaces;
using Solana.Web.Admin.Models.Requests.UserMessages;
using Solana.Web.Admin.Models.Responses.UserMessages;

namespace Solana.Web.Admin.API.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class UserMessagesController : ControllerBase
    {
        private readonly IUserMessagesLogic _logic;

        public UserMessagesController(IUserMessagesLogic logic)
        {
            _logic = logic;
        }

        /// <summary>
        /// Gets the user send box messages.
        /// OLD: GetUserSendBoxMessages
        /// </summary>
        /// <param name="admUserId">The adm user identifier.</param>
        /// <returns></returns>
        [HttpGet("UserSendBoxMessages")]
        public async Task<ActionResult<GetUserSendBoxMessagesResponse>> GetUserSendBoxMessages(int admUserId)
        {
            return await _logic.GetUserSendBoxMessages(admUserId);
        }

        /// <summary>
        /// Gets the user conversations.
        /// old: GetUserConversations
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        [HttpGet("UserConversations")]
        public async Task<ActionResult<GetUserConversationsResponse>> GetUserConversations([FromQuery] GetUserConversationsRequest request)
        {
            return await _logic.GetUserConversations(request);
        }

        /// <summary>
        /// Gets the user inbox box messages.
        /// old: GetUserInboxBoxMessages
        /// </summary>
        /// <param name="admUserId">The adm user identifier.</param>
        /// <returns></returns>
        [HttpGet("UserInboxBoxMessages")]
        public async Task<ActionResult<GetUserInboxBoxMessagesResponse>> GetUserInboxBoxMessages(int admUserId)
        {
            return await _logic.GetUserInboxBoxMessages(admUserId);
        }

        /// <summary>
        /// Return reply the message.
        /// old: ReplyMessage
        /// </summary>
        /// <param name="admMessageId"></param>
        /// <returns></returns>
        [HttpGet("ReplyMessage")]
        public async Task<ActionResult<GetReplyMessageResponse>> ReplyMessage(int admMessageId)
        {
            return await _logic.ReplyMessage(admMessageId);
        }

        /// <summary>
        /// Gets the show users.
        /// old: ShowUsers
        /// </summary>
        /// <returns></returns>
        [HttpGet("ShowUsers")]
        public async Task<ActionResult<GetShowUsersResponse>> GetShowUsers()
        {
            return await _logic.GetShowUsers();
        }

        /// <summary>
        /// Sends the message update.
        /// old: SendMessageUpdate
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        [HttpPost("SendMessageUpdate")]
        public async Task<ActionResult<bool>> SendMessageUpdate(PostMessageUpdateRequest request)
        {
            return await _logic.SendMessageUpdate(request);
        }

        /// <summary>
        /// Sends the reply update.
        /// old: SendReplyUpdate
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        [HttpPut("SendReplyUpdate")]
        public async Task<ActionResult<bool>> SendReplyUpdate(PutReplyUpdateRequest request)
        {
            return await _logic.SendReplyUpdate(request);
        }

        /// <summary>
        /// Sets the is read.
        /// old: SetIsRead
        /// </summary>
        /// <param name="admMessageId">The adm message identifier.</param>
        /// <returns></returns>
        [HttpPut("SetIsRead")]
        public async Task<ActionResult<bool>> SetIsRead(int admMessageId)
        {
            return await _logic.SetIsRead(admMessageId);
        }
    }
}
