using F_DAL.DTO.Request;
using F_DAL.DTO.Response;
using F_DAL.Models;
using Mapster;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
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
        private readonly IEmailSender emailSender;

        public AutharizationService(UserManager<ApplecationUser> userManager,IConfiguration configuration,IEmailSender emailSender)
        {
            _userManager = userManager;
            _configuration = configuration;
            this.emailSender = emailSender;
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
                var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                token = Uri.EscapeDataString(token);
                var emailUrl = $"https://localhost:7104/api/auth/Account/ConfirmEmail?token={token}&userId={user.Id}";


                await emailSender.SendEmailAsync(
                    user.Email,
                    "welcome",
                    $"<h1>Welcome {user.UserName}</h1><a href=\"{emailUrl}\">Confirm Email</a>");

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
        public async Task<bool> ConfirmEmail(string token, string userId)
        {
            var user =await _userManager.FindByIdAsync(userId);
            if (user == null) {
                return false;
            }
            var result =await _userManager.ConfirmEmailAsync(user, userId);
            if (!result.Succeeded)
            {
                return false;
            }
            return true;
        }


        private async Task<string> TokenAccess(ApplecationUser User)
        {
            var roles = await _userManager.GetRolesAsync(User);
            var userClaims = new List<Claim>() { 
            new Claim ("id",User.Id.ToString()),
            new Claim ("UserName",User.UserName?? ""),
            new Claim ("email",User.Email?? ""),
            new Claim("Roles",string.Join(",",roles)),
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
        public async Task<ForgitPasswordResponse> RequestPasswordReset(ForgitPasswordRequest request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if(user is null)
            {
                return new ForgitPasswordResponse
                {
                    Message = "not found email",
                    Sucsess = false
                };
            }
            var random = new Random();
            var code = random.Next(1000,9999).ToString();

            user.CodeResetPassword = code;
            user.paaswordResetDateExpiry = DateTime.UtcNow.AddMinutes(15);


            await _userManager.UpdateAsync(user);

            await emailSender.SendEmailAsync(request.Email, "reset password", $"cod is {code}");

            return new ForgitPasswordResponse
            {
                Message = "code sent to email",
                Sucsess = true
            };





        }


        public async Task<ResetPasswordResponse> resetpassword(ResetPasswordRequest request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user is null)
            {
                return new ResetPasswordResponse
                {
                    Message = "not found email",
                    Sucsess = false
                };
            }

            if(user.CodeResetPassword != request.Code)
            {
                return new ResetPasswordResponse
                {
                    Message = "invalid code",
                    Sucsess = false
                };
            }
            else if(user.paaswordResetDateExpiry < DateTime.UtcNow)
            {

                return new ResetPasswordResponse
                {
                    Message = " code expired",
                    Sucsess = false
                };
            }
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);

            var result =await _userManager.ResetPasswordAsync(user, token, request.Password);


            if (!result.Succeeded)
            {
                return new ResetPasswordResponse
                {
                    Message = "reset password is faild",
                    Sucsess = false,
                    Errors = result.Errors.Select(e=>e.Description).ToList()
                };

            }

            await emailSender.SendEmailAsync(request.Email, "change password", "change password is done");


            return new ResetPasswordResponse
            {
                Message = "reset password is sucsess",
                Sucsess = true
            };





        }

    }
}
