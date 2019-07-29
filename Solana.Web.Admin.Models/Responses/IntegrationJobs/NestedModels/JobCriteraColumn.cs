using System.Collections.Generic;

namespace Solana.Web.Admin.Models.Responses.IntegrationJobs.NestedModels
{
  public  class JobCriteraColumn
    {
        public string ColumnName { get; set; }
        public ICollection<JobCriteriaColumnValue> ColumnCriteriaValues { get; set; }

    }
}
