#pragma warning disable CS8618 // Suppress "Non-nullable property must contain a non-null value" warnings


using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace User.Gestion.Data.Models
{
    public class TicketResponse
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public string Response { get; set; } = string.Empty;

        public DateTime ResponseDate { get; set; }

        [ForeignKey("TicketId")]
        public Ticket Ticket { get; set; } = new Ticket();

        public Guid TicketId { get; set; }
    }

}
#pragma warning restore CS8618
