using System;
using Horizon.Common.Repository.Legacy.Models.Common;

namespace Solana.Web.Admin.Models.Responses.IntegrationJobs.NestedModels
{
    public class AdmScheduledTaskItem
    {
        public int AdmScheduledTaskID { get; set; }
        public ScheduledTaskType AdmScheduledTaskType { get; set; }
        public int AdmIntegrationJobID { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime StartTime { get; set; }
        public int? Frequency { get; set; }
        public bool RunOnStartup { get; set; }
    }
}