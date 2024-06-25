using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using User.Gestion.Data.Models;

namespace User.Gestion.Data.Models
{
    public class Sinistre
    {
        public int Id { get; set; }

        [Required]
        public string NumeroDossier { get; set; }

        [Required]
        public DateTime DateDeclaration { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public SinistreStatut Statut { get; set; }

        [Required]
        public decimal MontantEstime { get; set; }  //L'utilisateur estime combien le sinistre pourrait coûter. Ce montant est sa meilleure estimation avant que l'assurance ne prenne une décision.

        public decimal MontantPaye { get; set; } //C'est le montant effectivement payé par l'assurance. Initialement, il peut être 0 et sera mis à jour une fois que l'assurance aura déterminé et payé le montant final du sinistre.

        [Required]
        public string UserId { get; set; }

        public ApplicationUser User { get; set; }
    }
}