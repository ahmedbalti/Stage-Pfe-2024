using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace User.Gestion.Data.Models
{
    public class TicketResponseDTO
    {
        public Guid Id { get; set; }

        public string Response { get; set; }

        public Guid TicketId { get; set; }

        // Ajoutez d'autres propriétés si nécessaire
    }
}
