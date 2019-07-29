using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Horizon.Common.Repository.Legacy.Models.Adm;
using Horizon.Common.Repository.Legacy.Models.Inv;

namespace Solana.Web.Admin.Models.Requests.Schools.NestedModels
{
    public class AdmSiteModel
    {
        [Required]
        public int AdmSiteId { get; set; }
        public int? ParentAdmSiteId { get; set; }
        [Required]
        public int AdmManagementLevelId { get; set; }
        public int? MenAgeGroupId { get; set; }
        [Required]
        public string SiteId { get; set; }
        public string SiteDescription { get; set; }
        public decimal? DailyOverhead { get; set; }
        public int? MealsServed { get; set; }
        public int? ServeDays { get; set; }
        public bool? ServeMonday { get; set; }
        public bool? ServeTuesday { get; set; }
        public bool? ServeWednesday { get; set; }
        public bool? ServeThursday { get; set; }
        public bool? ServeFriday { get; set; }
        public bool? ServeSaturday { get; set; }
        public bool? ServeSunday { get; set; }
        public bool? IsManagementLayer { get; set; }
        public bool? IsBusinessUnit { get; set; }
        public bool? SevereNeed { get; set; }
        public int? Enrollment { get; set; }
        public decimal? AttendanceFactor { get; set; }
        public int? ApprovedFree { get; set; }
        public int? ApprovedReduced { get; set; }
        public int InvSiteTypeId { get; set; }
        public decimal? AverageCostPerLaborHour { get; set; }
        public decimal? SpecialFoodAllowance { get; set; }
        public decimal? SuppFoodAllowance { get; set; }
        public byte? StartingDayOfWeek { get; set; }
        public decimal? BasicDailyFoodAllowance { get; set; }
        public string LastAdjustmentItemEntry { get; set; }
        public string RegistrationCode { get; set; }
        public string Activation { get; set; }
        public string LicenseFile { get; set; }
        public string LastRun { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsDistrict { get; set; }
        public string ParentSiteDescription { get; set; }
        public bool SelectSite { get; set; }
        public string State { get; set; }
        public virtual AdmManagementLevel AdmManagementLevel { get; set; }
        public virtual InvSiteType InvSiteType { get; set; }
        public virtual ICollection<AdmSite> ChildAdmSites { get; set; }
        public virtual AdmSite ParentAdmSite { get; set; }
        public virtual ICollection<AdmUsersActivity> AdmUsersActivities { get; set; }
        public virtual ICollection<AdmUser> AdmUsers { get; set; }
        public virtual ICollection<AdmOperationalCalendar> AdmOperationalCalendars { get; set; }
        public virtual AdmSitesOption AdmSitesOption { get; set; }
        public virtual ICollection<AdmSitesServingPeriod> AdmSitesServingPeriods { get; set; }
    }
}
