using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Helper.Models;
using Helper.Services.interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Helper.Controllers
{
    [ApiController]
    [Route("api/sent-mail")]
    public class SentMailController : ControllerBase
    {
        private readonly ISentMailService _sentMailService;
        public SentMailController(ISentMailService sentMailService)
        {
            _sentMailService = sentMailService;
        }
        // send mail
        [HttpPost("")]
        public async Task<IActionResult> SendMail([FromBody] MailRequest mailRequest)
        {
            if (mailRequest.AutoGenerateOpt)
            {
                var response = await _sentMailService.SendMailOtpAsync(mailRequest.MailAccount, mailRequest.SentMail, mailRequest.AutoGenerateOpt);
                if (response.Status == true)
                {
                    return Ok(response);
                }
                else
                {
                    return BadRequest(response);
                }
            }
            else
            {
                var response = await _sentMailService.SendMailAsync(mailRequest.MailAccount, mailRequest.SentMail);
                if (response.Status == true)
                {
                    return Ok(response);
                }
                else
                {
                    return BadRequest(response);
                }
            }
        }
        // check otp
        [HttpPost("check-otp")]
        public IActionResult CheckOtp([FromBody] MailCheckOtpRequest mailCheckOtpRequest)
        {
            var response = _sentMailService.CheckOtp(mailCheckOtpRequest.Otp, mailCheckOtpRequest.Email, mailCheckOtpRequest.Now);
            if (response.Status == true)
            {
                return Ok(response);
            }
            else
            {
                return BadRequest(response);
            }
        }
    }
}