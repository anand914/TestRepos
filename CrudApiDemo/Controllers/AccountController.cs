using CrudDemoDataAccessLayer.ViewModel;
using CrudDemoServicesLayer.Services.IService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace CrudApiDemo.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IEmail _emailsrvc;
        public AccountController(IEmail emailService)
        {
            _emailsrvc = emailService;
        }

        [HttpPost]
        public async Task<IActionResult>SendMail([FromForm] MailRequest request)
        {
            try
            {
                await _emailsrvc.SendEmailAsync(request);
                return Ok();
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        [HttpPost("welcome")]
        public async Task<IActionResult> SendWelcomeMail([FromForm] WelcomeRequest request)
        {
            try
            {
                await _emailsrvc.SendWelcomeEmailAsync(request);
                return Ok();
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> SendOTP([FromForm]OtpVM model)
        {
            try
            {
                await _emailsrvc.SendOtp(model);
                return Ok();
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
    }
}
