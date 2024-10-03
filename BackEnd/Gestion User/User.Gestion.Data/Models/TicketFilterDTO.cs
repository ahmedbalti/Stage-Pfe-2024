#pragma warning disable CS8618 // Suppress "Non-nullable property must contain a non-null value" warnings


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace User.Gestion.Data.Models
{
    public class TicketFilterDTO
    {
        public TicketTitle? Title { get; set; }
        public TicketStatut? Status { get; set; }
        // Ajoutez d'autres propriétés de filtre si nécessaire
    }
}
#pragma warning restore CS8618
