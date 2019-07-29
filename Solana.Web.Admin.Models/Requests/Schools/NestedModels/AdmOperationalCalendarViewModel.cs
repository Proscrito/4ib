using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Solana.Web.Admin.Models.Requests.Schools.NestedModels
{
    public class AdmOperationalCalendarViewModel
    {
        public int AdmSiteId { get; set; }
        public bool? CreatedBySite { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public string SchoolStartDate { get; set; } 
        public string SchoolEndDate { get; set; }
        public List<AdmOperationalCalendarModel> AdmOperationalCalendar { get; set; }
        public bool IsYearRoundSchool { get; set; }
        public string DefaultStartDate { get; set; }

        public AdmOperationalCalendarViewModel()
        {
            AdmOperationalCalendar = new List<AdmOperationalCalendarModel>();
        }
    }
}