using Helper.Models;
using System.Text.Json;

namespace Helper
{
    public class OTP
    {
        /// <summary>
        /// Generate OTP with given length
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string GenerateOTP(int length)
        {
            const string chars = "0123456789";
            Random random = new Random();
            char[] otp = new char[length];
            for (int i = 0; i < length; i++)
            {
                otp[i] = chars[random.Next(chars.Length)];
            }

            return new string(otp);
        }
        // save otp to json file
        public static void SaveOtpToJsonFile(string otp, string email, DateTime expiredAt)
        {
            var otpModel = new OtpModel
            {
                Email = email,
                Otp = otp,
                ExpiredAt = expiredAt,
                IsUsed = false
            };
            // save otp to json file

            var otps = JsonSerializer.Deserialize<List<OtpModel>>(File.ReadAllText(Contains.PathOtp));
            if (otps == null && !otp.Any())
            {
                var otpsNew = new List<OtpModel>();
                otpsNew.Add(otpModel);
                var otpsJson = JsonSerializer.Serialize(otpsNew);
                System.IO.File.WriteAllText(Contains.PathOtp, otpsJson);
            }
            else
            {
                var otpsNew = new List<OtpModel>();
                otpsNew.Add(otpModel);
                otpsNew.AddRange(otps);
                var otpsJson = JsonSerializer.Serialize(otpsNew);
                System.IO.File.WriteAllText(Contains.PathOtp, otpsJson);
            }

        }
    }
}