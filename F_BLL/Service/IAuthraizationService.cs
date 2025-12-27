using F_DAL.DTO.Request;
using F_DAL.DTO.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace F_BLL.Service
{
    public interface IAuthraizationService
    {
        Task<RegisterResponse>registerAsync(RegisterRequest request);
        Task<LoginResponse>LoginAsync(LoginRequest request);
        public  Task<bool> ConfirmEmail(string token, string userId);
        Task<ForgitPasswordResponse> RequestPasswordReset(ForgitPasswordRequest request);
        Task<ResetPasswordResponse> resetpassword(ResetPasswordRequest request);
    }
}
