using System;
using System.Collections.Generic;

namespace WebAPIBiz4Company.Models
{
    public partial class JobApplier
    {
        public JobApplier()
        {
            JobApplieds = new HashSet<JobApplied>();
        }

        public int JobApplierId { get; set; }
        public string JobApplierFullname { get; set; } = null!;
        public string JobApplierEmail { get; set; } = null!;
        public string JobApplierPhoneNumber { get; set; } = null!;
        public string JobApplierAddress { get; set; } = null!;
        public string JobApplierPresentCompany { get; set; } = null!;
        public int JobApplierExperience { get; set; }
        public string JobApplierCv { get; set; } = null!;
        public int? JobApplierBeInformed { get; set; }
        public int? JobApplierJob { get; set; }

        public virtual Job? JobApplierJobNavigation { get; set; }
        public virtual ICollection<JobApplied> JobApplieds { get; set; }
    }
}
