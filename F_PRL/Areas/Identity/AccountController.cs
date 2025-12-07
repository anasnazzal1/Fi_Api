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
            return Ok(result);
        }
        
    }
}
