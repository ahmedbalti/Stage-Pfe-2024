using Microsoft.AspNetCore.Identity;


namespace User.Gestion.Data.Models
{
    public class ApplicationUser: IdentityUser
    {
        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenExpiry { get; set; }

        public ICollection<Ticket> Tickets { get; set; }

    }
}
