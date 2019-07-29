namespace Solana.Web.Admin.Models.Responses.YearEndProcess
{
    public class SaveYearEndProcessSetupResponse
    {
        public int OptionsForTheYear { get; set; }
        public bool IsPromoteStudents { get; set; }
        public bool IsPurgeGraduates { get; set; }
        public int AccountBalanceOption { get; set; }
        public string DefaultStartDate { get; set; }
        public string DefaultEndDate { get; set; }
        public string DefaultStartAltDate { get; set; }
        public string DefaultTempStatusExpDate { get; set; }
        public int? LastUpdatedBy { get; set; }
        public string LastUpdatedDate { get; set; }
        public string ErrorMsgString { get; set; }
        public bool HasYearRoundSchool { get; set; }
        public bool IsPurgeHHMembers { get; set; }
        public string RolloverExecuted { get; set; }
        public bool IsBreakupDCHouseholds { get; set; }
        public string RolloverStart { get; set; }
        public long StartingAppNumber { get; set; }
    }
}
