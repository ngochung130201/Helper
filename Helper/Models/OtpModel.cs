using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Helper.Models
{
    public class OtpModel
    {
        public string? Email { get; set; }
        public string? Otp { get; set; }
        public DateTime? ExpiredAt { get; set; }
        public bool? IsUsed { get; set; }

    }
}