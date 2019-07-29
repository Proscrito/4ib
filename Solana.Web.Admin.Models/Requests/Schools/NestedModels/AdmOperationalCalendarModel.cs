namespace Solana.Web.Admin.Models.Requests.Schools.NestedModels
{
    public class AdmOperationalCalendarModel
    {
        public string DayDescription { get; set; }
        public int OperationalStatus { get; set; }
        // this is for the calendar to pull out the date as a string
        public string OperationalDateString { get; set; }
    }
}