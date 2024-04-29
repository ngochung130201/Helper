using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Helper.Models
{
    public class MailAccountModel
    {
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? SenderName { get; set; }
        public string? SenderEmail { get; set; }
        public string? SmtpServer { get; set; }
        public int SmtpPort { get; set; }

    }
}