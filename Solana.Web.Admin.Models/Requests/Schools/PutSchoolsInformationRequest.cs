using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Horizon.Common.Repository.Legacy.Models.Adm;
using Horizon.Common.Repository.Legacy.Models.Inv;
using Horizon.Common.Repository.Legacy.Models.Men;
using Solana.Web.Admin.Models.Requests.Schools.NestedModels;

namespace Solana.Web.Admin.Models.Requests.Schools
{
    public class PutSchoolsInformationRequest
    {
        public int AdmSiteId { get; set; }
        [Required(ErrorMessage = "Please Enter School")]
        public string SiteId { get; set; }
        public int InvSiteTypeId { get; set; }
        [Required(ErrorMessage = "Please Enter School Name")]
        public string SiteDescription { get; set; }
        public bool IsActive { get; set; }
        public bool IsDistrict { get; set; }
        public string State { get; set; }
        public int AdmManagementLevelId { get; set; }
        public AdmSite Site { get; set; }
        public int ParentAdmSiteId { get; set; }
        public int AdmUserId { get; set; }
        public string ParentAdmSiteDesc { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string ZipCode { get; set; }
        public string ManagerName { get; set; }
        public string ManagerEmail { get; set; }
        public string ManagerPhoneNumber { get; set; }
        public string SchoolPhoneNumber { get; set; }
        public string AccLockoutDate { get; set; }
        public bool IsWarehouse { get; set; }
        public List<AdmSiteModel> AdmSites { get; set; }
        public List<InvSiteType> InvSiteType { get; set; }
        public List<AdmManagementLevel> AdmManagementLevel { get; set; }
        public List<AdmState> States { get; set; }
        public AdmSitesPinConfigurationViewModel PinConfiguration { get; set; }
        public AdmOperationalCalendarViewModel Calendar { get; set; }
        public List<SchoolSiteServingPeriod> SiteServingPeriods { get; set; }
        public string SiteServingPeriodData { get; set; }
        public List<AdmSite> ParentAdmSiteList { get; set; }
        public UserSitesViewModel UserSiteModel { get; set; }
        public int TimeZoneOffset { get; set; }
        public List<TerminalModel> Terminals { get; set; }
        public string TerminalPostString { get; set; }
        public string ErrorMsgString { get; set; }
        public List<GradeTransfer> GradeTransfer { get; set; }
        public List<ServingSites> ServingSites { get; set; }
        public SiteOptionsModel SiteOptions { get; set; }
        public int? MenAgeGroupID { get; set; }
        public string CustomerNumber { get; set; }

        public List<TimeZoneViewModel> TimeZoneList => new List<TimeZoneViewModel>
        {
            new TimeZoneViewModel("Eastern Standard Time"), //Eastern Time (UTC‌-05:00)
            new TimeZoneViewModel("Central Standard Time"), //Central Time (UTC‌-06:00)
            new TimeZoneViewModel("Mountain Standard Time"), //Mountain Time (UTC‌-07:00)
            new TimeZoneViewModel("US Mountain Standard Time"), //Arizona (UTC‌-07:00)
            new TimeZoneViewModel("Pacific Standard Time"), //Pacific Time (UTC‌-08:00)
            new TimeZoneViewModel("Alaskan Standard Time"), //Alaska (UTC‌-09:00)
            new TimeZoneViewModel("Hawaiian Standard Time") //Hawaii (UTC‌-10:00)
            
        };

        public string TimeZoneName { get; set; }
        public string COTimeZoneName { get; set; }

        public List<MenAgeGroups> MenAgeGroups { get; set; }
        public List<SchoolServingPeriod> SchoolServingPeriod { get; set; }
        public int DistrictId { get; set; }
        public int CEPOption { get; set; }
        public PutSchoolsInformationRequest()
        {
            AdmSites = new List<AdmSiteModel>();
            InvSiteType = new List<InvSiteType>();
            AdmManagementLevel = new List<AdmManagementLevel>();
            States = new List<AdmState>();
            SiteServingPeriods = new List<SchoolSiteServingPeriod>();
            ParentAdmSiteList = new List<AdmSite>();
            UserSiteModel = new UserSitesViewModel();
            GradeTransfer = new List<GradeTransfer>();
            ServingSites = new List<ServingSites>();
            SiteOptions = new SiteOptionsModel();
            MenAgeGroups = new List<MenAgeGroups>();
            SchoolServingPeriod = new List<SchoolServingPeriod>();
        }
    }
}
