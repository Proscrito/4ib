using System.Collections.Generic;
using Solana.Web.Admin.Models.Responses.UserMessages.NestedModels;

namespace Solana.Web.Admin.Models.Responses.UserMessages
{
    public class GetShowUsersResponse
    {
        public ICollection<ShowUserItem> ShowedUsers { get; set; }
    }
}