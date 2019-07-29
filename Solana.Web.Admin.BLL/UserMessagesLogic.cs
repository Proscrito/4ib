using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using AutoMapper;
using Horizon.Common.Repository.Legacy;
using Horizon.Common.Repository.Legacy.Models.Adm;
using Solana.Web.Admin.BLL.Interfaces;
using Solana.Web.Admin.Models.Requests.UserMessages;
using Solana.Web.Admin.Models.Responses.UserMessages;
using Solana.Web.Admin.Models.Responses.UserMessages.NestedModels;

namespace Solana.Web.Admin.BLL
{
    public class UserMessagesLogic : IUserMessagesLogic
    {
        private readonly ISolanaRepository _repository;
        private readonly IMapper _autoMapper;

        public UserMessagesLogic(ISolanaRepository repository, IMapper autoMapper)
        {
            _repository = repository;
            _autoMapper = autoMapper;
        }

        public async Task<GetUserSendBoxMessagesResponse> GetUserSendBoxMessages(int admUserId)
        {
            return new GetUserSendBoxMessagesResponse
            {
                SentMessages = (await _repository.GetListAsync<AdmMessagesDetail>(msg => msg.SentByAdmUserID == admUserId))
                              .OrderByDescending(msg => msg.MessageDate)
                              .Select(msg => new UserMessage
                              {
                                  AdmMessageId = msg.AdmMessageID,
                                  UserName = msg.AdmUser.FirstName + " " + msg.AdmUser.LastName,
                                  SiteDescription = msg.AdmMessage.AdmSite.SiteDescription,
                                  GroupName = msg.AdmMessage.AdmUserGroup.GroupName,
                                  MessageDate = msg.MessageDate,
                                  IsRead = msg.IsRead,
                                  IsUrgent = msg.AdmMessage.IsUrgent
                              })
                              .ToList()
            };
        }

        public async Task<GetUserConversationsResponse> GetUserConversations(GetUserConversationsRequest request)
        {
            return new GetUserConversationsResponse
            {
                Conversations = (await _repository.GetListAsync<AdmMessagesDetail>(msg =>
                        msg.AdmMessageID == request.MessageId))
                    .OrderByDescending(msg => msg.MessageDate)
                    .Select(m => new ConversationModel
                    {
                        AdmMessagesDetailID = m.AdmMessagesDetailID,
                        AdmMessageID = m.AdmMessageID,
                        UserName = m.AdmUser.FirstName + " " + m.AdmUser.LastName,
                        MesageText = m.MesageText,
                        IsRead = m.SentByAdmUserID == request.AdmUserId || m.IsRead,
                        IsForwarded = m.IsForwarded,
                        MessageDate = m.MessageDate
                    })
                    .ToList()
            };
        }

        public async Task<GetUserInboxBoxMessagesResponse> GetUserInboxBoxMessages(int admUserId)
        {
            return new GetUserInboxBoxMessagesResponse
            {
                InboxMessages = (await _repository.GetListAsync<AdmMessage>(m =>
                        (m.AdmUserID != admUserId && m.Recipients.Any(r => r.AdmUserID == admUserId))
                        | (m.AdmUserID == admUserId && (m.AdmMessagesReplies.Count > 0))))
                    .OrderByDescending(msg => msg.MessageDate)
                    .Select(m => new UserMessage
                    {
                        AdmMessageId = m.AdmMessageID,
                        AdmUserId = m.AdmUserID,
                        UserName = m.AdmUser.FirstName + " " + m.AdmUser.LastName,
                        SiteDescription = m.AdmSite.SiteDescription,
                        GroupName = m.AdmUserGroup.GroupName,
                        MessageDate = m.MessageDate,
                        IsRead = !m.AdmMessagesDetails.Any(d =>
                            d.SentByAdmUserID != admUserId && !d.IsRead),
                        IsUrgent = m.IsUrgent
                    })
                    .ToList()
            };
        }

        public async Task<GetReplyMessageResponse> ReplyMessage(int admMessageId)
        {
            //Create MessageDetailsModel object
            GetReplyMessageResponse model = new GetReplyMessageResponse();

            //Create a list object of type int
            List<int> userIDsList = new List<int>();

            //Create a list object of type string
            List<string> userNamesList = new List<string>();

            //Make call to repo to get the AdmMessageDetail object by ID
            var message = await _repository.FindAsync<AdmMessagesDetail>(admMessageId);

            //Check to see if the object is not null
            if (message != null)
            {
                //Get the AdmMessageId and store into the request
                model.AdmMessageId = message.AdmMessageID;

                //Add the AdmUserID to the List of Int (userIDsList)
                userIDsList.Add(message.AdmUser.AdmUserID);

                //Add the First and Last Name of the Adm User
                userNamesList.Add(message.AdmUser.FirstName + " " + message.AdmUser.LastName);

                //Checks Message Recipients to ensure it's not null and that there are recipients
                if (message.AdmMessage.Recipients != null && message.AdmMessage.Recipients.Any())
                {
                    //Loops through AdmUser object and adds the AdmUserID to the userIDsList
                    //Also adds the First Name and Last Name  to the userNamesList
                    foreach (var user in message.AdmMessage.Recipients)
                    {
                        userIDsList.Add(user.AdmUserID);
                        userNamesList.Add(user.FirstName + " " + user.LastName);
                    }
                }
            }

            model.UserIDs = String.Join(",", userIDsList);
            model.UserNames = String.Join(", ", userNamesList);

            //Ensures the message isn't null and adds the MessageText to the request
            if (message != null) model.Message = RemoveHtmlTags(message.MesageText);
            return model;
        }

        public async Task<GetShowUsersResponse> GetShowUsers()
        {
            return new GetShowUsersResponse()
            {
                ShowedUsers =
                    (await _repository.GetListAsync<AdmUser>(u =>
                        u.UserLogin != null &&
                        u.Active &&
                        u.IsDeleted == false &&
                        u.AllowLogin))
                    .Select(m => new ShowUserItem
                    {
                        AdmUserID = m.AdmUserID,
                        UserName = m.FirstName + " " + m.LastName
                    })
                    .ToList()
            };
        }

        public async Task<bool> SendMessageUpdate(PostMessageUpdateRequest request)
        {
            string message = request.Message;

            int admUserId = request.AdmUserId;
            int admSiteId = request.AdmSiteId;

            AdmMessage admMessage = _autoMapper.Map<AdmMessage>(request);
            admMessage.MessageDate = _repository.GetSchoolNow(admSiteId);

            foreach (var id in request.UserIDs)
            {
                AdmUser admUser = await _repository.GetAsync<AdmUser>(u => u.AdmUserID == id);
                admMessage.Recipients.Add(admUser);
            }

            bool isForwarded = request.ActionType.Equals("forwarded");
            admMessage.AdmMessagesDetails.Add(new AdmMessagesDetail
            {
                AdmMessageID = admMessage.AdmMessageID,
                SentByAdmUserID = admUserId,
                MesageText = message.Trim(),
                MessageDate = _repository.GetSchoolNow(admSiteId),
                IsForwarded = isForwarded
            });

            await _repository.CreateAsync<AdmMessage>(admMessage);
            return true;
        }

        public async Task<bool> SendReplyUpdate(PutReplyUpdateRequest request)
        {
            var now = _repository.GetSchoolNow(request.AdmSiteId);

            var message = await _repository.FindAsync<AdmMessage>(request.AdmMessageId);
            if (message != null)
            {
                if (message.AdmUserID != request.AdmUserId && message.AdmMessagesReplies != null &&
                    !message.AdmMessagesReplies.Any())
                {
                    // add record to AdmMessagesReply to indicate the very first reply,
                    // so it would be displayed in addressee's inbox
                    AdmMessagesReply admMessagesReply = new AdmMessagesReply
                    {
                        AdmMessageID = request.AdmMessageId,
                        SentToAdmUserID = message.AdmUserID,
                        SentByAdmUserID = request.AdmUserId,
                        SiteDescription = request.SiteName,
                        GroupName = request.GroupName,
                        UserName = request.FirstName + " " + request.LastName,
                        MessageDate = now
                    };

                    message.AdmMessagesReplies.Add(admMessagesReply);
                }

                // add details
                AdmMessagesDetail admMessagesDetail =
                    new AdmMessagesDetail
                    {
                        AdmMessageID = request.AdmMessageId,
                        SentByAdmUserID = request.AdmUserId,
                        MesageText = request.Message,
                        MessageDate = now
                    };
                message.AdmMessagesDetails.Add(admMessagesDetail);

                // reset message IsRead flag
                message.IsRead = false;

                // update the whole message and all its parts
                await _repository.UpdateAsync(message);

                // reset reply IsRead flag
                var reply = await _repository.GetAsync<AdmMessagesReply>(m =>
                     m.AdmMessageID == request.AdmMessageId);
                if (reply != null)
                {
                    reply.IsRead = false;
                    await _repository.UpdateAsync(reply);
                }
            }

            return true;
        }

        public async Task<bool> SetIsRead(int admMessageId)
        {
            // update messages
            var message = await _repository.FindAsync<AdmMessage>(admMessageId);
            if (message != null)
            {
                message.IsRead = true;
                foreach (var det in message.AdmMessagesDetails)
                {
                    det.IsRead = true;
                }

            }

            // update replies
            if (message != null && message.AdmMessagesReplies != null)
            {
                foreach (var reply in message.AdmMessagesReplies)
                {
                    reply.IsRead = true;
                }
            }

            await _repository.UpdateAsync<AdmMessage>(message);
            return true;
        }

        /// <summary>
        /// This method takes in a string i.e. MessageText
        /// and removes the html markup using Regex. It then
        /// returns the string value w/o the markup
        /// </summary>
        /// <param name="html"></param>
        /// <returns>string value w/o HTML Markup</returns>
        private static string RemoveHtmlTags(string html)
        {
            string onlyText = Regex.Replace(html, @"<(.|\n)*?>", string.Empty);
            return onlyText;
        }
    }
}