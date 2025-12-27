using F_BLL.Service;
using F_DAL.DTO.Request;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;

namespace F_PRL.Areas.Identity
{
    [Route("api/auth/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        public readonly IAuthraizationService _authraizationService;

        public AccountController(IAuthraizationService authraizationService)
        {
            _authraizationService = authraizationService;
        }
        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterRequest request)
        {
            var result = await _authraizationService.registerAsync(request);
            if (!result.Sucsess)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            var result = await _authraizationService.LoginAsync(request);
            if (!result.Sucsess)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
        [HttpPost("ConfirmEmail")]
        public async Task<IActionResult> ConfirmEmail(string token, string userId)
        {
            var result = await _authraizationService.ConfirmEmail(token, userId);
           
            return Ok(result);
        }
        [HttpPost("SendCode")]
        public async Task<IActionResult> requestPasswordReset(ForgitPasswordRequest request)
        {
            var result = await _authraizationService.RequestPasswordReset(request);
            if(!result.Sucsess)
            {
                return BadRequest(result);
            }

            return Ok(result);

        }

        [HttpPatch("ResetPassword")]
        public async Task<IActionResult> ResetPassword(ResetPasswordRequest request)
        {
            var result = await _authraizationService.resetpassword(request);
            if (!result.Sucsess)
            {
                return BadRequest(result);
            }

            return Ok(result);

        }


    }
}
