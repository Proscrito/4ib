using System.ComponentModel.DataAnnotations;

namespace Solana.Web.Admin.Models.Responses.ManagementLevels.NestedModels
{
    public class AdmManagementLevelViewModel
    {
        public int AdmManagementLevelID { get; set; }

        [StringLength(50, ErrorMessage = "Description should be 50 chars or less")]
        public string Description { get; set; }

        [Required]
        public int ManagementLevelOrder { get; set; }
    }
}
