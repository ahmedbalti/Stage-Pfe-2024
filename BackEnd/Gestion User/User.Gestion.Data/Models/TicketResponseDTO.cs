#pragma warning disable CS8618 // Suppress "Non-nullable property must contain a non-null value" warnings

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

        public string Response { get; set; } = string.Empty;

        public Guid TicketId { get; set; }

        // Ajoutez d'autres propriétés si nécessaire
    }
}
#pragma warning restore CS8618
