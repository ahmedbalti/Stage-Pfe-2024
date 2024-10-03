#pragma warning disable CS8618 // Suppress "Non-nullable property must contain a non-null value" warnings

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace User.Gestion.Data.Models
{
    public class TicketStatusUpdateDTO
    {
        public string Statut { get; set; } = string.Empty;// Utilisez une chaîne pour les statuts
    }
}
#pragma warning restore CS8618
