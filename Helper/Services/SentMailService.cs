using System.Reflection.Metadata;
using System.Text.Json;
using Helper;
using Helper.Models;
using Helper.Services.interfaces;
using MailKit.Net.Smtp;
using MimeKit;
using MimeKit.Text;

namespace Helper.Services
{
    public class SentMailService : ISentMailService
    {
        public async Task<ApiResponseModel> SendMailAsync(MailAccountModel mailAccount, SentMailModel sentMail)
        {
            var response = new ApiResponseModel();
            try
            {
                var result = await HelperSendMail(mailAccount, sentMail);
                if (result)
                {
                    response.Message = "Mail sent successfully";
                    response.Status = true;
                }
                else
                {
                    response.Message = "Mail sending failed";
                    response.Status = false;
                }
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.Status = false;
            }
            return response;
        }

        public Task<ApiResponseModel> SendMailOtpAsync(MailAccountModel mailAccount, SentMailModel sentMail, bool autoGenerateOpt)
        {
            if (autoGenerateOpt)
            {

                var otp = OTP.GenerateOTP(6);
                sentMail.Body = sentMail.Body?.Replace("{{otp}}", $"{otp}");
                OTP.SaveOtpToJsonFile(otp: otp, email: sentMail.To, expiredAt: DateTime.Now.AddMinutes(5));
            }
            return SendMailAsync(mailAccount, sentMail);
        }
        public async Task<bool> HelperSendMail(MailAccountModel mailAccount, SentMailModel sentMail)
        {
            try
            {
                var host = mailAccount.SmtpServer;
                var port = mailAccount.SmtpPort;
                var username = mailAccount.Email;
                var password = mailAccount.Password;
                var senderName = mailAccount.SenderName;
                var senderEmail = mailAccount.SenderEmail;
                var message = new MimeMessage();
                message.From.Add(new MailboxAddress(senderName, senderEmail));
                message.To.Add(new MailboxAddress(sentMail.Name, sentMail.To));
                message.Subject = sentMail.Subject;
                message.Body = new TextPart(TextFormat.Html)
                {
                    Text = sentMail.Body
                };
                using (var client = new SmtpClient())
                {
                    client.Connect(host, port);
                    client.Authenticate(username, password);
                    await client.SendAsync(message);
                    client.Disconnect(true);
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public ApiResponseModel CheckOtp(string otp, string email, DateTime now)
        {
            var otps = JsonSerializer.Deserialize<List<OtpModel>>(File.ReadAllText(Contains.PathOtp));
            if (otps == null)
            {
                return new ApiResponseModel
                {
                    Message = "OTP not found",
                    Status = false
                };
            }
            var otpModel = otps.Find(x => x.Email == email && x.Otp == otp && x.ExpiredAt > now && x.IsUsed == false);
            if (otpModel != null)
            {
                otpModel.IsUsed = true;
                var otpsJson = JsonSerializer.Serialize(otps);
                File.WriteAllText(Contains.PathOtp, otpsJson);
                return new ApiResponseModel
                {
                    Message = "OTP is valid",
                    Status = true
                };
            }
            else
            {
                return new ApiResponseModel
                {
                    Message = "OTP is invalid",
                    Status = false
                };
            }

        }
    }
}