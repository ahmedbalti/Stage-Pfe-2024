using Gestion_User.Models.Authentication.SignUp;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using User.Gestion.Service.Models;
using User.Gestion.Service.Models.Authentication.User;

namespace User.Gestion.Service.Services
{
    public interface IUserManagement
    {

        Task<ApiResponse<CreateUserResponse>> CreateUserWithTokenAsync(RegisterUser registerUser);

        Task<ApiResponse<List<string>>> AssignRoleToUserAsync(List<string> roles, IdentityUser user);
    }
}
