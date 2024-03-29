﻿using System;
using System.Collections.Generic;

namespace WebAPIBiz4Company.Models
{
    public partial class JobType
    {
        public JobType()
        {
            Job = new HashSet<Job>();
        }

        public int JobTypeId { get; set; }
        public string JobTypeName { get; set; }

        public virtual ICollection<Job> Job { get; set; }
    }
}
