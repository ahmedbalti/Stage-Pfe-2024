using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using User.Gestion.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace User.Gestion.Service.Services
{
    public class TicketService : ITicketService
    {
        private readonly ApplicationDbContext _context;

        public TicketService(ApplicationDbContext context)
        {
            _context = context;
        }

        //public async Task<Ticket> AddTicketAsync(Ticket ticket)
        //{
        //    if (ticket == null)
        //    {
        //        throw new ArgumentNullException(nameof(ticket));
        //    }

        //    _context.Tickets.Add(ticket);
        //    await _context.SaveChangesAsync();
        //    return ticket;
        //}


        public async Task<Ticket> AddTicketAsync(Ticket ticket)
        {
            if (ticket == null)
            {
                throw new ArgumentNullException(nameof(ticket));
            }

            // Vérifiez que l'ID du propriétaire est bien défini
            if (string.IsNullOrEmpty(ticket.OwnerId))
            {
                throw new InvalidOperationException("OwnerId must be set.");
            }

            _context.Tickets.Add(ticket);
            await _context.SaveChangesAsync();
            return ticket;
        }

        public async Task<IEnumerable<Ticket>> GetTicketsByUserIdAsync(string userId)
        {
            return await _context.Tickets.Where(t => t.OwnerId == userId).ToListAsync();
        }

        //public async Task<Ticket> GetTicketByIdAsync(Guid id)
        //{
        //    var ticket = await _context.Tickets
        //        .Include(t => t.Owner) // Inclure le propriétaire du ticket
        //        .FirstOrDefaultAsync(t => t.IdTicket == id);
        //    if (ticket == null)
        //    {
        //        throw new NullReferenceException("Ticket not found");
        //    }
        //    return ticket;
        //}

        public async Task<Ticket> GetTicketByIdAsync(Guid id)
        {
            var ticket = await _context.Tickets
                .Include(t => t.Owner) // Inclure le propriétaire du ticket
                .FirstOrDefaultAsync(t => t.IdTicket == id);

            if (ticket == null)
            {
                throw new NullReferenceException("Ticket not found");
            }

            return ticket;
        }


        public async Task<Ticket> UpdateTicketAsync(Guid id, Ticket ticket)
        {
            var existingTicket = await _context.Tickets.FindAsync(id);
            if (existingTicket == null)
            {
                return null;
            }

            existingTicket.Titre = ticket.Titre;
            existingTicket.Description = ticket.Description;
            existingTicket.Priority = ticket.Priority;
            existingTicket.Statut = ticket.Statut;
            existingTicket.ResolutionDate = ticket.ResolutionDate;

            _context.Tickets.Update(existingTicket);
            await _context.SaveChangesAsync();

            return existingTicket;
        }

        public async Task<IEnumerable<Ticket>> GetTicketsByTitleAndUserIdAsync(TicketTitle title, string userId)
        {
            return await _context.Tickets
                .Where(t => t.OwnerId == userId && t.Titre == title)
                .ToListAsync();
        }

        public async Task<IEnumerable<Ticket>> GetAllTicketsAsync()
        {
            return await _context.Tickets.ToListAsync();
        }

        public async Task<IEnumerable<Ticket>> FilterTicketsAsync(TicketFilterDTO filter)
        {
            var query = _context.Tickets.AsQueryable();

            if (filter.Title.HasValue)
            {
                query = query.Where(t => t.Titre == filter.Title);
            }

            if (filter.Status.HasValue)
            {
                query = query.Where(t => t.Statut == filter.Status);
            }

            // Ajoutez d'autres filtres ici si nécessaire

            return await query.ToListAsync();
        }

        public async Task<Ticket> RespondToTicketAsync(Guid id, TicketResponseDTO responseDTO)
        {
            var ticket = await _context.Tickets.FindAsync(id);
            if (ticket == null)
            {
                return null;
            }

            // Ajoutez la logique pour ajouter une réponse au ticket
            // Par exemple, ajouter la réponse à une liste de réponses dans le ticket
            ticket.Responses.Add(new TicketResponse { Response = responseDTO.Response, ResponseDate = DateTime.UtcNow });

            await _context.SaveChangesAsync();

            return ticket;
        }



        public async Task<bool> UpdateTicketStatus(Guid ticketId, TicketStatut statut)
        {
            var ticket = await _context.Tickets.FindAsync(ticketId);
            if (ticket == null)
            {
                return false;
            }

            ticket.Statut = statut;

            if (statut == TicketStatut.Resolu)
            {
                ticket.ResolutionDate = DateTime.UtcNow;
            }
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<TicketResponse>> GetResponsesByTicketIdAsync(Guid ticketId)
        {
            return await _context.TicketResponses.Where(r => r.TicketId == ticketId).ToListAsync();
        }

        public async Task<IEnumerable<TicketResponse>> GetAllTicketResponsesAsync(Guid ticketId)
        {
            return await _context.TicketResponses.Where(tr => tr.TicketId == ticketId).ToListAsync();
        }

        public async Task<IEnumerable<TicketResponse>> GetClientTicketResponsesAsync(Guid ticketId, string userId)
        {
            var ticket = await GetTicketByIdAsync(ticketId);
            if (ticket == null || ticket.OwnerId != userId)
            {
                return new List<TicketResponse>();
            }

            return await _context.TicketResponses.Where(tr => tr.TicketId == ticketId).ToListAsync();
        }

        public async Task<TicketStatisticsDTO> GetTicketStatisticsAsync()
        {
            var totalTickets = await _context.Tickets.CountAsync();
            var resolvedTickets = await _context.Tickets.CountAsync(t => t.Statut == TicketStatut.Resolu);
            var unresolvedTickets = totalTickets - resolvedTickets;

            var lowPriorityTickets = await _context.Tickets.CountAsync(t => t.Priority == TicketPriority.Faible);
            var mediumPriorityTickets = await _context.Tickets.CountAsync(t => t.Priority == TicketPriority.Moyenne);
            var highPriorityTickets = await _context.Tickets.CountAsync(t => t.Priority == TicketPriority.Haute);

            return new TicketStatisticsDTO
            {
                TotalTickets = totalTickets,
                ResolvedTickets = resolvedTickets,
                UnresolvedTickets = unresolvedTickets,
                LowPriorityTickets = lowPriorityTickets,
                MediumPriorityTickets = mediumPriorityTickets,
                HighPriorityTickets = highPriorityTickets
            };
        }



    }
}
