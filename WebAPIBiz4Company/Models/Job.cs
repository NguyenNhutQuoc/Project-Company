using System;
using System.Collections.Generic;

namespace WebAPIBiz4Company.Models
{
    public partial class Job
    {
        public Job()
        {
            JobApplied = new HashSet<JobApplied>();
            JobDescriptionNavigation = new HashSet<JobDescription>();
        }

        public int JobId { get; set; }
        public string JobName { get; set; }
        public string JobDescription { get; set; }
        public string JobAddress { get; set; }
        public string JobWorkingForm { get; set; }
        public DateTime JobDateCreated { get; set; }
        public int JobType { get; set; }
        public DateTime? JobDateUpdated { get; set; }

        public virtual JobType JobTypeNavigation { get; set; }
        public virtual ICollection<JobApplied> JobApplied { get; set; }
        public virtual ICollection<JobDescription> JobDescriptionNavigation { get; set; }
    }
}
