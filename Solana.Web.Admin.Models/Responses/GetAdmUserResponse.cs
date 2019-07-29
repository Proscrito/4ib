using System;

namespace Solana.Web.Admin.Models.Responses
{
    public class GetAdmUserResponse
    {
        public int AdmUserID { get; set; }
        public bool HasAdmUserGroup { get; set; }
        public bool IsLocked { get; set; }
        public DateTime? LastLoginFailure { get; set; }
        public int? NumberOfFailedLoginAttempts { get; set; }
        public string Password { get; set; }
        public string UserLogin { get; set; }
    }
}
