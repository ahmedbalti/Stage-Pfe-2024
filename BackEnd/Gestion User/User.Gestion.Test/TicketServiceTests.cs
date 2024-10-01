using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using User.Gestion.Data.Models;
using User.Gestion.Service.Services;
using Xunit;

namespace User.Gestion.Test
{
    public class TicketServiceTests
    {
        private readonly ApplicationDbContext _context;
        private readonly TicketService _service;

        public TicketServiceTests()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()) // Unique database for each test
                .Options;

            _context = new ApplicationDbContext(options);
            _context.Database.EnsureDeleted(); // Clear the database before each test
            _context.Database.EnsureCreated();
            _service = new TicketService(_context);
        }

        [Fact]
        public async Task AddTicketAsync_AddsTicketToContext()
        {
            // Arrange
            var ticket = new Ticket
            {
                Titre = TicketTitle.DemandeAide,
                Description = "This is a test ticket",
                Priority = TicketPriority.Faible,
                Statut = TicketStatut.Nouveau,
                OwnerId = "test-user-id",
                CreatedOn = DateTime.UtcNow
            };

            // Act
            var result = await _service.AddTicketAsync(ticket);

            // Assert
            Assert.Equal(1, _context.Tickets.Count());
            Assert.Equal("This is a test ticket", result.Description);
        }

        [Fact]
        public async Task GetTicketsByUserIdAsync_ReturnsTicketsForUser()
        {
            // Arrange
            var userId = "test-user-id";
            _context.Tickets.Add(new Ticket
            {
                Titre = TicketTitle.DemandeInformation,
                Description = "User's first ticket",
                Priority = TicketPriority.Moyenne,
                Statut = TicketStatut.EnCours,
                OwnerId = userId,
                CreatedOn = DateTime.UtcNow
            });
            _context.Tickets.Add(new Ticket
            {
                Titre = TicketTitle.Reclamation,
                Description = "User's second ticket",
                Priority = TicketPriority.Haute,
                Statut = TicketStatut.Nouveau,
                OwnerId = userId,
                CreatedOn = DateTime.UtcNow
            });
            await _context.SaveChangesAsync();

            // Act
            var result = await _service.GetTicketsByUserIdAsync(userId);

            // Assert
            Assert.Equal(2, result.Count());
            Assert.All(result, t => Assert.Equal(userId, t.OwnerId));
        }

      

        [Fact]
        public async Task UpdateTicketAsync_UpdatesTicketInContext()
        {
            // Arrange
            var ticket = new Ticket
            {
                Titre = TicketTitle.Reclamation,
                Description = "Initial description",
                Priority = TicketPriority.Faible,
                Statut = TicketStatut.Nouveau,
                OwnerId = "test-user-id",
                CreatedOn = DateTime.UtcNow
            };
            _context.Tickets.Add(ticket);
            await _context.SaveChangesAsync();

            // Act
            ticket.Description = "Updated description";
            var result = await _service.UpdateTicketAsync(ticket.IdTicket, ticket);

            // Assert
            Assert.NotNull(result);
            var updatedTicket = await _context.Tickets.FindAsync(ticket.IdTicket);
            Assert.Equal("Updated description", updatedTicket.Description);
        }

        [Fact]
        public async Task GetAllTicketsAsync_ReturnsAllTickets()
        {
            // Arrange
            _context.Tickets.Add(new Ticket
            {
                Titre = TicketTitle.DemandeInformation,
                Description = "First ticket",
                Priority = TicketPriority.Faible,
                Statut = TicketStatut.Nouveau,
                OwnerId = "user1",
                CreatedOn = DateTime.UtcNow
            });
            _context.Tickets.Add(new Ticket
            {
                Titre = TicketTitle.Reclamation,
                Description = "Second ticket",
                Priority = TicketPriority.Moyenne,
                Statut = TicketStatut.EnCours,
                OwnerId = "user2",
                CreatedOn = DateTime.UtcNow
            });
            await _context.SaveChangesAsync();

            // Act
            var result = await _service.GetAllTicketsAsync();

            // Assert
            Assert.Equal(2, result.Count());
        }
    }
}
