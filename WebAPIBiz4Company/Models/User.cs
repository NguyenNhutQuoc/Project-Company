using System;
using System.Collections.Generic;

namespace WebAPIBiz4Company.Models
{
    public partial class User
    {
        public int UserId { get; set; }
        public string? UserFullname { get; set; }
        public string? UserEmail { get; set; }
        public string? UserPhoneNumber { get; set; }
        public string? UserCompanyName { get; set; }
        public string? UserQuestion { get; set; }
    }
}
