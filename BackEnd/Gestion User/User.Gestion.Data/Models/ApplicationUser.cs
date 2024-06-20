using Microsoft.AspNetCore.Identity;


namespace User.Gestion.Data.Models
{
    public class ApplicationUser: IdentityUser
    {
        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenExpiry { get; set; }

        public ICollection<Ticket> Tickets { get; set; }

        public ICollection<Contract> Contracts { get; set; }  // Ajout de cette ligne

        public ICollection<Sinistre> Sinistres { get; set; }

        public ICollection<Devis> Devis { get; set; }



    }
}
