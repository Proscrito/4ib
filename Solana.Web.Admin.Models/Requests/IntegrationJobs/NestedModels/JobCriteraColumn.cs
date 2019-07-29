using System.Collections.Generic;

namespace Solana.Web.Admin.Models.Requests.IntegrationJobs.NestedModels
{
  public  class JobCriteraColumn
    {
        public string ColumnName { get; set; }
        public ICollection<JobCriteriaColumnValue> ColumnCriteriaValues { get; set; }

    }
}
