using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solana.Web.Admin.Models.Requests.Schools
{
    public class PutSiteCalendarRequest
    {
        public int FromAdmSiteId { get; set; }
        public int[] ToAdmSiteIds { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string SchoolStartDate { get; set; }
        public string SchoolEndDate { get; set; }
    }
}
