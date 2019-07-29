using System.ComponentModel.DataAnnotations;

namespace Solana.Web.Admin.Models.Requests.Schools.NestedModels
{
    public class TerminalModel
    {
        public int PosTerminalId { get; set; }
        public int AdmSiteId { get; set; }
        [RegularExpression(@"^\d{1,10}$", ErrorMessage = "Only numbers are allowed for Terminal Number")]
        public int TerminalNumber { get; set; }
        public string MachineName { get; set; }
        public string ClientVersion { get; set; }
        public bool IsActive { get; set; }
        public bool IsAutoSale { get; set; }
        public bool IsDeleted { get; set; }
    }
}