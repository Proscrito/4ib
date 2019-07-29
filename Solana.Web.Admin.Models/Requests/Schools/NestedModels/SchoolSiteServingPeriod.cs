using System;

namespace Solana.Web.Admin.Models.Requests.Schools.NestedModels
{
    public class SchoolSiteServingPeriod
    {
        public int AdmServingPeriodId { get; set; }
        public string Description { get; set; }
        public int AdmSiteId { get; set; }
        public bool IsUseDefaultForAllDays { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public DateTime MondayStartTime { get; set; }
        public DateTime MondayEndTime { get; set; }
        public DateTime TuesdayStartTime { get; set; }
        public DateTime TuesdayEndTime { get; set; }
        public DateTime WednesdayStartTime { get; set; }
        public DateTime WednesdayEndTime { get; set; }
        public DateTime ThursdayStartTime { get; set; }
        public DateTime ThursdayEndTime { get; set; }
        public DateTime FridayStartTime { get; set; }
        public DateTime FridayEndTime { get; set; }
        public DateTime SaturdayStartTime { get; set; }
        public DateTime SaturdayEndTime { get; set; }
        public DateTime SundayStartTime { get; set; }
        public DateTime SundayEndTime { get; set; }
        public string StartTimeStr { get; set; }
        public string EndTimeStr { get; set; }
        public string MondayStartTimeStr { get; set; }
        public string MondayEndTimeStr { get; set; }
        public string TuesdayStartTimeStr { get; set; }
        public string TuesdayEndTimeStr { get; set; }
        public string WednesdayStartTimeStr { get; set; }
        public string WednesdayEndTimeStr { get; set; }
        public string ThursdayStartTimeStr { get; set; }
        public string ThursdayEndTimeStr { get; set; }
        public string FridayStartTimeStr { get; set; }
        public string FridayEndTimeStr { get; set; }
        public string SaturdayStartTimeStr { get; set; }
        public string SaturdayEndTimeStr { get; set; }
        public string SundayStartTimeStr { get; set; }
        public string SundayEndTimeStr { get; set; }

        public bool IsNew { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsSelected { get; set; }
    }
}