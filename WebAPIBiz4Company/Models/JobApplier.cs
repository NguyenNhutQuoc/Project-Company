using System;
using System.Collections.Generic;

namespace WebAPIBiz4Company.Models
{
    public partial class JobApplier
    {
        public JobApplier()
        {
            JobApplied = new HashSet<JobApplied>();
        }

        public int JobApplierId { get; set; }
        public string JobApplierFullname { get; set; }
        public string JobApplierEmail { get; set; }
        public string JobApplierPhoneNumber { get; set; }
        public string JobApplierAddress { get; set; }
        public string JobApplierPresentCompany { get; set; }
        public int JobApplierExperience { get; set; }
        public string JobApplierCv { get; set; }
        public int? JobApplierBeInformed { get; set; }

        public virtual ICollection<JobApplied> JobApplied { get; set; }
    }
}
