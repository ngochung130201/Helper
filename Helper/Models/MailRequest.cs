
using System.ComponentModel.DataAnnotations;

namespace Helper.Models
{
    public class MailRequest
    {
        public MailAccountModel MailAccount { get; set; }
        public SentMailModel SentMail { get; set; }
        public bool AutoGenerateOpt { get; set; } = false;
    }
    public class MailCheckOtpRequest
    {
        [Required, MaxLength(6, ErrorMessage = "OTP must be 6 characters long")]
        public string Otp { get; set; }
        [EmailAddress, Required]
        public string Email { get; set; }
        public DateTime Now { get; set; }
    }
}