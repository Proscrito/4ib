using System.Collections.Generic;
using Solana.Web.Admin.Models.Requests.OnlineApplication.NestedModels;

namespace Solana.Web.Admin.Models.Requests.OnlineApplication
{
    public class PutAdmFroOptionsRequest
    {
        public bool SupportsSnap { get; set; }
        public bool SupportsTANF { get; set; }
        public bool SupportsFDPIR { get; set; }
        public int LengthSnap { get; set; }
        public int LengthTANF { get; set; }
        public int LengthFDPIR { get; set; }
        public string StartsWithSnap { get; set; }
        public string StartsWithTANF { get; set; }
        public string StartsWithFDPIR { get; set; }

        public string ProgramSnap { get; set; }
        public string ProgramTANF { get; set; }
        public string ProgramFDPIR { get; set; }

        public string DescriptionSnap { get; set; }
        public string DescriptionTANF { get; set; }
        public string DescriptionFDPIR { get; set; }
        public bool DisclosureConsent { get; set; }

        public int? MinLengthSNAP { get; set; }
        public int? MaxLengthSNAP { get; set; }
        public int? MinLengthTANF { get; set; }
        public int? MaxLengthTANF { get; set; }
        public int? MinLengthFDPIR { get; set; }
        public int? MaxLengthFDPIR { get; set; }

        public bool DisplayFoster { get; set; }
        public bool DisplayHomeless { get; set; }
        public bool DisplayMigrant { get; set; }
        public bool DisplayRunaway { get; set; }
        public bool HideDistrict { get; set; }

        public ICollection<FroCustomizationSaveModel> Customizations { get; set; }
    }
}
