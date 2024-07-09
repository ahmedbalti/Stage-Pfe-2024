using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using User.Gestion.Data.Models;


namespace User.Gestion.Service.Services
{
    public interface ITicketService
    {
        Task<Ticket> AddTicketAsync(Ticket ticket);
        Task<IEnumerable<Ticket>> GetTicketsByUserIdAsync(string userId);
        Task<Ticket> GetTicketByIdAsync(Guid id);
        Task<Ticket> UpdateTicketAsync(Guid id, Ticket ticket);

        Task<IEnumerable<Ticket>> GetTicketsByTitleAndUserIdAsync(TicketTitle title, string userId);


    }
}

