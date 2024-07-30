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

        public string Response { get; set; }

        public DateTime ResponseDate { get; set; }

        [ForeignKey("TicketId")]
        public Ticket Ticket { get; set; }

        public Guid TicketId { get; set; }
    }

}
