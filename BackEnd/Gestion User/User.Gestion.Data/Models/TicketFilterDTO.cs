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
