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
        Task<IEnumerable<Ticket>> GetAllTicketsAsync();
        Task<IEnumerable<Ticket>> FilterTicketsAsync(TicketFilterDTO filter);
        Task<Ticket> RespondToTicketAsync(Guid id, TicketResponseDTO responseDTO);
        //  Task<Ticket> UpdateTicketStatusAsync(Guid id, TicketStatut statut);
        Task<bool> UpdateTicketStatus(Guid ticketId, TicketStatut statut);

        Task<IEnumerable<TicketResponse>> GetResponsesByTicketIdAsync(Guid ticketId);

        Task<IEnumerable<TicketResponse>> GetAllTicketResponsesAsync(Guid ticketId);
        Task<IEnumerable<TicketResponse>> GetClientTicketResponsesAsync(Guid ticketId, string userId);

        Task<TicketStatisticsDTO> GetTicketStatisticsAsync();


    }
}
