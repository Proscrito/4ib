using Horizon.Common.Repository.Legacy.Models.Common;

namespace Solana.Web.Admin.Models.Responses.OnlineApplications.NestedModels
{
    public class FroCustomizationViewModel
    {
        public int FroCustomizationID { get; set; }
        public string Name { get; set; }
        public FroCustomizationType Type { get; set; }
        public bool? IsActive { get; set; }
        public int? AppYear { get; set; }
    }
}
