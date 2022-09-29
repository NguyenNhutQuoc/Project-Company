using System;
using System.Collections.Generic;

namespace WebAPIBiz4Company.Models
{
    public partial class User
    {
        public int UserId { get; set; }
        public string UserFullname { get; set; } = null!;
        public string UserEmail { get; set; } = null!;
        public string UserPhoneNumber { get; set; } = null!;
        public string UserCompanyName { get; set; } = null!;
        public string? UserQuestion { get; set; }
    }
}
