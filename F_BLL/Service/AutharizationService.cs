using F_DAL.DTO.Request;
using F_DAL.DTO.Response;
using F_DAL.Models;
using Mapster;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace F_BLL.Service
{
    public class AutharizationService : IAuthraizationService
    {
        public readonly UserManager<ApplecationUser> _userManager;
        IConfiguration _configuration;

        public AutharizationService(UserManager<ApplecationUser> userManager,IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
        }

        public async Task<LoginResponse> LoginAsync(LoginRequest request)
        {
            try {
                    var user = await _userManager.FindByEmailAsync(request.Email);
                if (user == null) {
                    return new LoginResponse()
                    {
                        Sucsess = false,
                        Message = "unvalid username",
                        
                    };

                }
                var result = await _userManager.CheckPasswordAsync(user, request.Password);
                if (!result)
                {
                    return new LoginResponse()
                    {
                        Sucsess = false,
                        Message = "unvalid password",

                    };
                }
                return new LoginResponse()
                {
                    Sucsess = true,
                    Message = "login sucssesfuly",
                    acssessToken = await TokenAccess(user)
                };
            
            
            
            
            
            }
            catch (Exception ex) {
                return new LoginResponse()
                {
                    Sucsess = false,
                    Message = "error",
                    Errors = new List<string> { ex.Message }
                };

            }
        }

        public async Task<RegisterResponse> registerAsync(RegisterRequest request)
        {
            try
            {
                var user = request.Adapt<ApplecationUser>();
                var resule = await _userManager.CreateAsync(user, request.Password);
                if (!resule.Succeeded)
                {
                    return new RegisterResponse()
                    {
                        Sucsess = false,
                        Message = "error",
                        Errors = resule.Errors.Select(c => c.Description).ToList()
                    };
                }
                await _userManager.AddToRoleAsync(user, "User");
                return new RegisterResponse()
                {
                    Sucsess = true,
                    Message = "success"
                };
            }
            catch (Exception ex) {
                return new RegisterResponse()
                {
                    Sucsess = false,
                    Message = "error",
                    Errors = new List<string> { ex.Message }
                };

            }
        }


        private async Task<string> TokenAccess(ApplecationUser User)
        {
            var userClaims = new List<Claim>() { 
            new Claim ("id",User.Id.ToString()),
            new Claim ("UserName",User.UserName?? ""),
            new Claim ("email",User.Email?? "")
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSettings:SecretKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                    issuer: _configuration["JwtSettings:Issuer"],
                    audience: _configuration["JwtSettings:Audience"],
                    claims: userClaims,
                    expires: DateTime.UtcNow.AddMinutes(10),
                    signingCredentials: creds);
            return new JwtSecurityTokenHandler().WriteToken(token);


        }


    }
}
