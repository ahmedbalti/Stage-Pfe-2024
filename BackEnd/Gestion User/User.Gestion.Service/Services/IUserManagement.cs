using Gestion_User.Models.Authentication.Login;
using Gestion_User.Models.Authentication.SignUp;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using User.Gestion.Data.Models;
using User.Gestion.Service.Models;
using User.Gestion.Service.Models.Authentication.User;

namespace User.Gestion.Service.Services
{
    public interface IUserManagement
    {

        Task<ApiResponse<CreateUserResponse>> CreateUserWithTokenAsync(RegisterUser registerUser);

        Task<ApiResponse<List<string>>> AssignRoleToUserAsync(List<string> roles, ApplicationUser user);
      //  Task<ApiResponse<LoginOtpResponse>> GetOtpByLoginAsync(LoginModel loginModel);

        Task<ApiResponse<LoginResponse>> GetJwtTokenAsync(ApplicationUser user);
        Task<ApiResponse<LoginResponse>> LoginUserWithJWTokenAsync(string otp, string userName);
        Task<ApiResponse<LoginResponse>> RenewAccessTokenAsync(LoginResponse tokens);

        Task<List<ApplicationUser>> GetClientsAsync();

    }
}
