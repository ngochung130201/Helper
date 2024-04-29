using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Helper.Models;

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
            var otps = new List<OtpModel>();
            otps.Add(otpModel);
            var otpsJson = JsonSerializer.Serialize(otps);
            // otps from folder Assets
            System.IO.File.WriteAllText(Contains.PathOtp, otpsJson);
        }
    }
}