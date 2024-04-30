using Microsoft.AspNetCore.Identity;
using User.Gestion.Data.Models;

namespace User.Gestion.Service.Models.Authentication.User
{
    public class CreateUserResponse
    {
        public string Token { get; set; } = null!;
        public ApplicationUser User { get; set; } = null!;

    }
}
