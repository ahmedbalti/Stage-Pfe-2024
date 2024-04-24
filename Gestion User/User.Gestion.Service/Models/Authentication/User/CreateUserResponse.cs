using Microsoft.AspNetCore.Identity;

namespace User.Gestion.Service.Models.Authentication.User
{
    public class CreateUserResponse
    {
        public string Token { get; set;}
        public  IdentityUser User{ get; set; }

    }
}
