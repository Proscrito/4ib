using System;

namespace Solana.Web.Admin.Models.Responses
{
    /// <summary>
    /// This class encapsulates the GetAdmSiteResponse API response object format. 
    /// </summary>
    public class GetAdmSiteResponse
    {
        public int AdmSiteID { get; set; }
        public int? ParentAdmSiteID { get; set; }
        public int AdmManagementLevelID { get; set; }
        public string SiteID { get; set; }
        public string SiteDescription { get; set; }
        public int InvSiteTypeID { get; set; }
        public bool IsActive { get; set; }
        public bool IsDistrict { get; set; }
        public string State { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string ZipCode { get; set; }
        public string PrincipalName { get; set; }
        public string PrincipalEmail { get; set; }
        public string PrincipalPhoneNumber { get; set; }
        public int? MenAgeGroupID { get; set; }
        public DateTime? AccLockoutDate { get; set; }
        public string CustomerNumber { get; set; }
        public string SchoolPhoneNumber { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDateTime { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTime? LastModifiedDateTime { get; set; }
    }
}
