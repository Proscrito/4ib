using System;

namespace Solana.Web.Admin.Models.Requests.IntegrationJobs.NestedModels
{
    public class TaskModel
    {
        public int? Frequency { get; set; }
        public DateTime? StartDate { get; set; }
        public bool? RunOnStartup { get; set; }
        public TimeSpan? StartTime { get; set; }
    }
}