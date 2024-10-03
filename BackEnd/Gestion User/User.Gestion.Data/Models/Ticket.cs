#pragma warning disable CS8618 // Suppress "Non-nullable property must contain a non-null value" warnings


using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace User.Gestion.Data.Models
{
    public class Ticket
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid IdTicket { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public TicketTitle Titre { get; set; }

        [StringLength(1000)]
        public string Description { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? ResolutionDate { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public TicketPriority Priority { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public TicketStatut Statut { get; set; }

        [Required]
        public string OwnerId { get; set; } // Identifiant de l'utilisateur propriétaire du ticket

        [JsonIgnore] // Ignorer la propriété Owner pour éviter les cycles
        [ForeignKey("OwnerId")]
        public ApplicationUser Owner { get; set; } // Propriété de navigation vers l'utilisateur propriétaire

        [JsonIgnore]
        public List<TicketResponse> Responses { get; set; } = new List<TicketResponse>();

    }
}
#pragma warning restore CS8618
