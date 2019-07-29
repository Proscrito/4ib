using System;

namespace Solana.Web.Admin.Models
{
    public class PatchAdmUser
    {
        public int AdmUserID { get; set; }
        public string UserLogin { get; set; }
        public string Password { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string Email { get; set; }
        public bool Active { get; set; }
        public int PreferredHand { get; set; }
        public bool BlindBalancing { get; set; }
        public int? NumberOfFailedLoginAttempts { get; set; }
        public DateTime? LastLoginFailure { get; set; }
        public DateTime? PasswordLastChangedDate { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? CreatedByUserID { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int? ModifiedByUserID { get; set; }
        public bool IsDeleted { get; set; }
        public string UserPIN { get; set; }
        public bool AllowLogin { get; set; }
        public bool IsHidden { get; set; }
        public bool BlindBalancingOnTillReport { get; set; }
        public bool IsLocked { get; set; }
        public int? AdmUserGroupID { get; set; }
        public int? DaysActive { get; set; }
        public bool ReceiveImportExportNotifications { get; set; }
        public bool IsRefundFromPosEnabled { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDateTime { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTime? LastModifiedDateTime { get; set; }
    }
}
