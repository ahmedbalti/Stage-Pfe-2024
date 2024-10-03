#pragma warning disable CS8618 // Suppress "Non-nullable property must contain a non-null value" warnings


using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace User.Gestion.Data.Models
{
    public class Feedback
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [Range(1, 5)]
        public int Rating { get; set; }

        public string? Comment { get; set; }

        public DateTime DateCreated { get; set; }

        // Clé étrangère vers l'utilisateur qui a laissé le feedback
        [Required]
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
    }
}
#pragma warning restore CS8618
