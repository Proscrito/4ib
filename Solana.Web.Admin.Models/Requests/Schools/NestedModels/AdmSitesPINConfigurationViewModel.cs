namespace Solana.Web.Admin.Models.Requests.Schools.NestedModels
{
    public class AdmSitesPinConfigurationViewModel
    {
        public int AdmSiteId { get; set; }
        public int PINLength { get; set; }
        public bool PadPINWithZeros { get; set; }
        public bool PinsAreUniqueAcrossDistrict { get; set; }
        public bool AutoBringUpCustomerWhenPINLengthReached { get; set; }
        public bool StudentRandomlyGeneratePIN { get; set; }
        public bool StudentIsLeftOfId { get; set; }
        public int StudentLeftOfId { get; set; }
        public int StudentRightOfId { get; set; }
        public bool StudentAppendToPIN { get; set; }
        public bool StudentPreventLeadingZeros { get; set; }
        public string StudentAppendPosition { get; set; }
        public string StudentAppendText { get; set; }
        public bool UseAdultSettings { get; set; }
        public bool AdultRandomlyGeneratePIN { get; set; }
        public bool AdultIsLeftOfId { get; set; }
        public int AdultLeftOfId { get; set; }
        public int AdultRightOfId { get; set; }
        public bool AdultAppendToPIN { get; set; }
        public bool AdultPreventLeadingZeros { get; set; }
        public string AdultAppendPosition { get; set; }
        public string AdultAppendText { get; set; }
    }
}
