using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Solana.Web.Admin.Models.Requests.GlobalOptions.NestedModels;

namespace Solana.Web.Admin.Models.Requests.GlobalOptions
{
    //TODO: most of those fields are not used
    public class PutAdmGlobalOptionsRequest
    {
        [Required]
        public DateTime YearBeginsOn { get; set; }

        [Required]
        public int InactivityMinutesForLogoutTerminal { get; set; }

        [Required]
        public int InactivityMinutesForLogoutWeb { get; set; }

        [Required]
        public bool Display24HourTimeFormat { get; set; }

        [Required]
        public bool DetermineUserSecurityBySite { get; set; }

        [Required]
        public short NumberOfUnsuccessfulLoginAttemptsBeforeLockout { get; set; }

        [Required]
        public int LockoutDuration { get; set; }

        [Required]
        public byte ShouldTrackUserActivity { get; set; }

        public bool PosBlockAlaCarteCharging { get; set; }

        public bool GenerateAppNumbers { get; set; }

        public int MinAppNumberLength { get; set; }

        public int MaxAppNumberLength { get; set; }

        public bool InvConsolidateSiteOrders { get; set; }

        public bool InvManagerApproveOrders { get; set; }

        public bool IsPINUniqueAcrossTheDistrict { get; set; }

        [RegularExpression(@"^\d{1,10}$", ErrorMessage = "Only numbers are allowed for the starting application number")]
        public Int64 StartingAppNumber { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:0.0000}")]
        public decimal UniversalAttendanceFactor { get; set; }

        public bool UseUniversalAttendanceFactor { get; set; }

        public bool PosIsDrawerCompulsion { get; set; }

        public string AdjustmentReason { get; set; }

        //TODO: my guess it is used on UI as == 1 or == 2 and 1 is default. Sad but we should add initialization for compatibility
        public int UseCalendarDays { get; set; } = 1;

        public int? CEPSchoolOption { get; set; }

        public bool PosAlacarteCashAllowed { get; set; }

        public bool UseLeftoverCodes { get; set; }

        public string LeftoverCodes { get; set; }

        public bool HasProvision2Schools { get; set; }

        public string StateAgencyName { get; set; }

        public string SFAID { get; set; }

        public int SFAType { get; set; }

        public string SFACity { get; set; }

        public string SFAZipCode { get; set; }

        public int SodiumTargetId { get; set; }

        public bool IsStandAlone { get; set; }

        //AgeGroups only used when posting model back for save.
        public List<AgeGroupsViewSaveModel> AgeGroups { get; set; } = new List<AgeGroupsViewSaveModel>();

        public IEnumerable<CEPSchoolOptionsSaveModel> CepSchoolOptionsItemList { get; set; } = new List<CEPSchoolOptionsSaveModel>();
    }
}
