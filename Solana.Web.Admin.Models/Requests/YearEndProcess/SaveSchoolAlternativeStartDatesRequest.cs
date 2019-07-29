using Solana.Web.Admin.Models.Requests.YearEndProcess.NestedModels;
using System.Collections.Generic;

namespace Solana.Web.Admin.Models.Requests.YearEndProcess
{
    public class SaveSchoolAlternativeStartDatesRequest
    {
        public List<AdmConfigAlternativeStartDate> AlternativeStartDates { get; set; }
    }
}
