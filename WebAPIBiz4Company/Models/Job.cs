using System;
using System.Collections.Generic;

namespace WebAPIBiz4Company.Models
{
    public partial class Job
    {
        public Job()
        {
            JobApplieds = new HashSet<JobApplied>();
            JobDescriptions = new HashSet<JobDescription>();
        }

        public int JobId { get; set; }
        public string? JobName { get; set; }
        public string? JobDescription { get; set; }
        public string? JobAddress { get; set; }
        public string? JobWorkingForm { get; set; }
        public DateTime? JobDateCreated { get; set; }
        public int? JobType { get; set; }

        public virtual JobType? JobTypeNavigation { get; set; }
        public virtual ICollection<JobApplied> JobApplieds { get; set; }
        public virtual ICollection<JobDescription> JobDescriptions { get; set; }
    }
}
