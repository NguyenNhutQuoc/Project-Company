using System;
using System.Collections.Generic;

namespace WebAPIBiz4Company.Models
{
    public partial class Job
    {
        public Job()
        {
            JobApplieds = new HashSet<JobApplied>();
            JobAppliers = new HashSet<JobApplier>();
            JobDescriptions = new HashSet<JobDescription>();
        }

        public int JobId { get; set; }
        public string JobName { get; set; } = null!;
        public string JobDescription { get; set; } = null!;
        public string JobAddress { get; set; } = null!;
        public string JobWorkingForm { get; set; } = null!;
        public DateTime JobDateCreated { get; set; }
        public int JobType { get; set; }

        public virtual JobType JobTypeNavigation { get; set; } = null!;
        public virtual ICollection<JobApplied> JobApplieds { get; set; }
        public virtual ICollection<JobApplier> JobAppliers { get; set; }
        public virtual ICollection<JobDescription> JobDescriptions { get; set; }
    }
}
