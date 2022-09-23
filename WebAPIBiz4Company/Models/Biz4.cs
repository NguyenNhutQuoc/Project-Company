using System;
using System.Collections.Generic;

namespace WebAPIBiz4Company.Models
{
    public partial class Biz4
    {
        public int Id { get; set; }
        public string Fullname { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public string CompanyName { get; set; } = null!;
        public string Question { get; set; } = null!;
    }
}
