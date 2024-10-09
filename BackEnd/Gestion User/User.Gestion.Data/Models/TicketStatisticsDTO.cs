#pragma warning disable CS8618 // Suppress "Non-nullable property must contain a non-null value" warnings

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace User.Gestion.Data.Models
{
    public class TicketStatisticsDTO
    {
        public int TotalTickets { get; set; }
        public int ResolvedTickets { get; set; }
        public int UnresolvedTickets { get; set; }
        public int LowPriorityTickets { get; set; }
        public int MediumPriorityTickets { get; set; }
        public int HighPriorityTickets { get; set; }
    }
}
#pragma warning restore CS8618
