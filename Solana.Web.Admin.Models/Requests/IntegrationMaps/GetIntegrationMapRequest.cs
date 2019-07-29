using Horizon.Common.Repository.Legacy.Models.Common;

namespace Solana.Web.Admin.Models.Requests.IntegrationMaps
{
    public class GetIntegrationMapRequest
    {
        public int MapId { get; set; }
        public bool Import { get; set; }
        public IntegrationMapType MapType { get; set; }
    }
}
