using System.Collections.Generic;
using Solana.Web.Admin.Models.Responses.ManagementLevels.NestedModels;

namespace Solana.Web.Admin.Models.Responses.ManagementLevels
{
    public class GetAdmManagementLevelsResult
    {
        public ICollection<AdmManagementLevelViewModel> Items { get; set; }
    }
}
