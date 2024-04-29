using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Helper.Models;

namespace Helper.Services.interfaces
{
    public interface ISentMailService
    {
        public Task<ApiResponseModel> SendMailAsync(MailAccountModel mailAccount, SentMailModel sentMail);
        // SendMailAsync method with Auto Generate Opt
        public Task<ApiResponseModel> SendMailOtpAsync(MailAccountModel mailAccount, SentMailModel sentMail, bool autoGenerateOpt);
        // Check otp from json file
        public ApiResponseModel CheckOtp(string otp, string email, DateTime now);
    }
}