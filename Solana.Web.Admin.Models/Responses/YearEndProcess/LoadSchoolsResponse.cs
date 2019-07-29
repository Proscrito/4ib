using Solana.Web.Admin.Models.Responses.YearEndProcess.NestedModels;
using System.Collections.Generic;

namespace Solana.Web.Admin.Models.Responses.YearEndProcess
{
    public class LoadSchoolsResponse
    {
        public ICollection<PosGradeModel> Grades { get; set; }
        public ICollection<AdmSiteModel> Schools { get; set; }
    }
}
