using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace User.Gestion.Data.Models
{
    public class TicketDTO
    {
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public TicketTitle Titre { get; set; }

        [StringLength(1000)]
        public string Description { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public TicketPriority Priority { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public TicketStatut Statut { get; set; }

        public DateTime? ResolutionDate { get; set; }
    }
}
