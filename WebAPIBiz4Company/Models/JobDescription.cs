using System;
using System.Collections.Generic;

namespace WebAPIBiz4Company.Models
{
    public partial class JobDescription
    {
        public int Jdid { get; set; }
        public string? JdresonToJoinJob { get; set; }
        public string? JdjobDescription { get; set; }
        public string? JdskillsAndExperienceRequired { get; set; }
        public string? JdresonToLoveJob { get; set; }
        public int? Jdjob { get; set; }

        public virtual Job? JdjobNavigation { get; set; }
    }
}
