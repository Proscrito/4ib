using System;
using System.Collections.Generic;
using Horizon.Common.Repository.Legacy.Models.Adm;
using Horizon.Common.Repository.Legacy.Models.Pos;

namespace Solana.Web.Admin.Models.Requests.Schools.NestedModels
{
    public class GradeTransfer
    {
        public GradeTransfer()
        {
            PosGrade = new List<PosGrade>();
            AdmSite = new List<AdmSite>();
        }
        public int GradeTransferId { get; set; }
        public int PosGradeId { get; set; }
        public int FromAdmSiteId { get; set; }
        public int ToAdmSiteId { get; set; }
        public DateTime LastUpdateDate { get; set; }
        public int AdmUserId { get; set; }
        public bool IsDeleted { get; set; }
        public List<PosGrade> PosGrade { get; set; }
        public List<AdmSite> AdmSite { get; set; }
    }
}