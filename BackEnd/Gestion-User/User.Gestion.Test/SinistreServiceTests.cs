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

    public class SinistreServiceTests
    {
        private readonly ApplicationDbContext _context;
        private readonly SinistreService _service;

        public SinistreServiceTests()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()) // Unique database name for each test
                .Options;

            _context = new ApplicationDbContext(options);
            _context.Database.EnsureDeleted(); // Clear the database before each test
            _context.Database.EnsureCreated();
            _service = new SinistreService(_context);
        }

        [Fact]
        public async Task CreateSinistre_AddsSinistreToContext()
        {
            // Arrange
            var sinistre = new Sinistre
            {
                NumeroDossier = "Dossier123",
                DateDeclaration = DateTime.UtcNow,
                Description = "Test Sinistre",
                Statut = SinistreStatut.Ouverte,
                MontantEstime = 1000,
                MontantPaye = 0,
                UserId = "test-user-id"
            };

            // Act
            var result = await _service.CreateSinistre(sinistre);

            // Assert
            Assert.Equal(1, _context.Sinistres.Count());
            Assert.Equal("Dossier123", result.NumeroDossier);
        }

        [Fact]
        public async Task UpdateSinistre_UpdatesSinistreInContext()
        {
            // Arrange
            var sinistre = new Sinistre
            {
                NumeroDossier = "Dossier123",
                DateDeclaration = DateTime.UtcNow,
                Description = "Test Sinistre",
                Statut = SinistreStatut.Ouverte,
                MontantEstime = 1000,
                MontantPaye = 0,
                UserId = "test-user-id"
            };
            _context.Sinistres.Add(sinistre);
            await _context.SaveChangesAsync();

            // Act
            sinistre.Description = "Updated Description";
            var result = await _service.UpdateSinistre(sinistre);

            // Assert
            Assert.NotNull(result);
            var updatedSinistre = await _context.Sinistres.FindAsync(sinistre.Id);
            Assert.Equal("Updated Description", updatedSinistre.Description);
        }

        [Fact]
        public async Task GetSinistreById_ReturnsCorrectSinistre()
        {
            // Arrange
            var sinistre = new Sinistre
            {
                NumeroDossier = "Dossier123",
                DateDeclaration = DateTime.UtcNow,
                Description = "Test Sinistre",
                Statut = SinistreStatut.Ouverte,
                MontantEstime = 1000,
                MontantPaye = 0,
                UserId = "test-user-id"
            };
            _context.Sinistres.Add(sinistre);
            await _context.SaveChangesAsync();

            // Act
            var result = await _service.GetSinistreById(sinistre.Id, "test-user-id");

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Dossier123", result.NumeroDossier);
        }

        [Fact]
        public async Task GetSinistresByUser_ReturnsSinistresForUser()
        {
            // Arrange
            var userId = "test-user-id";
            _context.Sinistres.Add(new Sinistre
            {
                NumeroDossier = "Dossier123",
                DateDeclaration = DateTime.UtcNow,
                Description = "Test Sinistre 1",
                Statut = SinistreStatut.Ouverte,
                MontantEstime = 1000,
                MontantPaye = 0,
                UserId = userId
            });
            _context.Sinistres.Add(new Sinistre
            {
                NumeroDossier = "Dossier456",
                DateDeclaration = DateTime.UtcNow,
                Description = "Test Sinistre 2",
                Statut = SinistreStatut.EnCours,
                MontantEstime = 2000,
                MontantPaye = 0,
                UserId = userId
            });
            await _context.SaveChangesAsync();

            // Act
            var result = await _service.GetSinistresByUser(userId);

            // Assert
            Assert.Equal(2, result.Count);
            Assert.All(result, s => Assert.Equal(userId, s.UserId));
        }

        [Fact]
        public async Task GetAllSinistres_ReturnsAllSinistres()
        {
            // Arrange
            _context.Sinistres.Add(new Sinistre
            {
                NumeroDossier = "Dossier123",
                DateDeclaration = DateTime.UtcNow,
                Description = "Test Sinistre 1",
                Statut = SinistreStatut.Ouverte,
                MontantEstime = 1000,
                MontantPaye = 0,
                UserId = "user1"
            });
            _context.Sinistres.Add(new Sinistre
            {
                NumeroDossier = "Dossier456",
                DateDeclaration = DateTime.UtcNow,
                Description = "Test Sinistre 2",
                Statut = SinistreStatut.EnCours,
                MontantEstime = 2000,
                MontantPaye = 0,
                UserId = "user2"
            });
            await _context.SaveChangesAsync();

            // Act
            var result = await _service.GetAllSinistres();

            // Assert
            Assert.Equal(2, result.Count);
        }
    }
}
