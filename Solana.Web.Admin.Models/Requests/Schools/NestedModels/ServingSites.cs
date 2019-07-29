using System.Collections.Generic;
using Horizon.Common.Repository.Legacy.Models.Adm;

namespace Solana.Web.Admin.Models.Requests.Schools.NestedModels
{
    public class ServingSites
    {
        public ServingSites()
        {
            OtherAdmSite = new List<AdmSite>();
        }
        public int AdmSiteId { get; set; }
        public int OtherAdmSiteId { get; set; }
        public List<AdmSite> OtherAdmSite { get; set; }
    }
}