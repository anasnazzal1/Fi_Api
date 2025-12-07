using F_DAL.DTO.Request;
using F_DAL.DTO.Response;
using F_DAL.Models;
using Mapster;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace F_BLL.Service
{
    public class AutharizationService : IAuthraizationService
    {
        public readonly UserManager<ApplecationUser> _userManager;

        public AutharizationService(UserManager<ApplecationUser> userManager)
        {
            _userManager = userManager;
        }

        public Task<RegisterResponse> LoginAsync(LoginRequest request)
        {
            throw new NotImplementedException();
        }

        public async Task<RegisterResponse> registerAsync(RegisterRequest request)
        {
            var user = request.Adapt<ApplecationUser>();
            var resule  = await _userManager.CreateAsync(user,request.Password);
            if (!resule.Succeeded) {
                return new RegisterResponse()
                {
                    Message = "error"
                };
            }
            await _userManager.AddToRoleAsync(user, "User");
            return new RegisterResponse()
            {
                Message = "success"
            };
        }
    }
}
