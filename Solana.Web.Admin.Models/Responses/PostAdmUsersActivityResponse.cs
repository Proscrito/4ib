using System;

namespace Solana.Web.Admin.Models.Responses
{
    /// <summary>
    /// This class encapsulates the PostAdmUsersActivity API response object format. 
    /// It does not have to be the same as PostAdmUsersActivityRequest.
    /// </summary>
    public class PostAdmUsersActivityResponse
    {
        public int AdmUsersActivityID { get; set; }
        public int AdmUserID { get; set; }
        public int? AdmSiteID { get; set; }
        public string Activity { get; set; }
        public string IPAddress { get; set; }
        public DateTime ActivityDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDateTime { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTime? LastModifiedDateTime { get; set; }
    }
}
