using System.Collections.Generic;
using Solana.Web.Admin.Models.Requests.Schools.NestedModels;

namespace Solana.Web.Admin.Models.Requests.Schools
{
    public class PostMissingSchoolCalendarRequest
    {
        public List<AdmOperationalCalendarModel> Calendars { get; set; }
        public int AdmSiteId { get; set; }
    }
}
