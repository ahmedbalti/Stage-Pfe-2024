using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using User.Gestion.Data.Models;
using Gestion_User.Models;
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
        //    ticket.IdTicket = Guid.NewGuid();
        //    ticket.CreatedOn = DateTime.UtcNow;
        //    ticket.Statut = TicketStatut.Nouveau;

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

            _context.Tickets.Add(ticket);
            await _context.SaveChangesAsync();
            return ticket;
        }


        public async Task<IEnumerable<Ticket>> GetTicketsByUserIdAsync(string userId)
        {
            return await _context.Tickets.Where(t => t.OwnerId == userId).ToListAsync();
        }

        public async Task<Ticket> GetTicketByIdAsync(Guid id)
        {
            var ticket = await _context.Tickets.FindAsync(id);
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
    }
}

