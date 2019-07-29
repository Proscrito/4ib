using System;

namespace Solana.Web.Admin.Models.Requests.YearEndProcess
{
    public class GetSiteAlternativeDatesRequest
    {
        public DateTime StartDate { get; set; }
        public DateTime TempExpDate { get; set; }
    }
}
