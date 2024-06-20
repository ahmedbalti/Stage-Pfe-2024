using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

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
        public string Statut { get; set; }

        [Required]
        public decimal MontantEstime { get; set; }

        public decimal MontantPaye { get; set; }

        [JsonIgnore]
        public string UserId { get; set; }

        [JsonIgnore]
        public ApplicationUser ApplicationUser { get; set; }
    }
}
