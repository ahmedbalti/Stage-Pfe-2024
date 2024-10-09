#pragma warning disable CS8618 // Suppress "Non-nullable property must contain a non-null value" warnings


using System;
using System.ComponentModel.DataAnnotations;

namespace User.Gestion.Data.Models
{
    public class Opportunity
    {
        public int Id { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public decimal Montant { get; set; }

        [Required]
        public DateTime DateCreation { get; set; }

        [Required]
        public string AssuranceType { get; set; } // Type d'assurance: Auto, Habitation, Santé, Vie

        [Required]
        public decimal PrimeAnnuelle { get; set; } // Prime annuelle de l'assurance

        [Required]
        public int DureeContrat { get; set; } // Durée du contrat en mois

        [Required]
        public string Couverture { get; set; } // Détails de la couverture d'assurance

        [Required]
        public int DevisId { get; set; }
        public Devis Devis { get; set; }

        [Required]
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

        public bool IsApproved { get; set; } = false; // Indique si l'opportunité est approuvée
    }
}
#pragma warning restore CS8618
