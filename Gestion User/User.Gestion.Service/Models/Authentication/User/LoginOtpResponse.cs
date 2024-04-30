using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using User.Gestion.Data.Models;

namespace User.Gestion.Service.Models.Authentication.User
{
    public class LoginOtpResponse
    {
        public string Token { get; set; } = null!;
        public bool IsTwoFactorEnable { get; set; }

        public ApplicationUser User { get; set; } = null!;
    }
}
