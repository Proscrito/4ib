using System.Collections.Generic;
using Solana.Web.Admin.Models.Requests.ManagementLevels.NestedModels;

namespace Solana.Web.Admin.Models.Requests.ManagementLevels
{
    public class PutAdmManagementLevelsRequest
    {
        public ICollection<AdmManagementLevelSaveModel> Items { get; set; }
    }
}
