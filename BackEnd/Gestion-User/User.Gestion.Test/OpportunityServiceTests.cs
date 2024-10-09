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
    public class OpportunityServiceTests
    {
        private readonly ApplicationDbContext _context;
        private readonly OpportunityService _service;

        public OpportunityServiceTests()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()) // Unique database name for each test
                .Options;

            _context = new ApplicationDbContext(options);
            _context.Database.EnsureDeleted(); // Clear the database before each test
            _context.Database.EnsureCreated();
            _service = new OpportunityService(_context);
        }

        [Fact]
        public async Task GetOpportunitiesByUserId_ReturnsOpportunities()
        {
            // Arrange
            var userId = "test-user-id";
            _context.Opportunities.Add(new Opportunity
            {
                UserId = userId,
                Description = "Test Opportunity 1",
                Montant = 1000,
                DateCreation = DateTime.Now,
                AssuranceType = "Auto",
                PrimeAnnuelle = 100,
                DureeContrat = 12,
                Couverture = "Couverture de base",
                DevisId = 1
            });

            _context.Opportunities.Add(new Opportunity
            {
                UserId = userId,
                Description = "Test Opportunity 2",
                Montant = 2000,
                DateCreation = DateTime.Now,
                AssuranceType = "Sante",
                PrimeAnnuelle = 200,
                DureeContrat = 24,
                Couverture = "Couverture complète",
                DevisId = 2
            });

            await _context.SaveChangesAsync();

            // Act
            var result = await _service.GetOpportunitiesByUserId(userId);

            // Assert
            Assert.Equal(2, result.Count());
            Assert.All(result, o => Assert.Equal(userId, o.UserId));
        }


        [Fact]
        public async Task GetOpportunityById_ReturnsCorrectOpportunity()
        {
            // Arrange
            var opportunity = new Opportunity
            {
                Description = "Test Opportunity",
                UserId = "test-user-id",
                Montant = 1000,
                DateCreation = DateTime.Now,
                AssuranceType = "Auto",
                PrimeAnnuelle = 100,
                DureeContrat = 12,
                Couverture = "Couverture de base",
                DevisId = 1
            };

            _context.Opportunities.Add(opportunity);
            await _context.SaveChangesAsync();

            // Act
            var result = await _service.GetOpportunityById(opportunity.Id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Test Opportunity", result.Description);
        }


        [Fact]
        public async Task CreateOpportunitiesForDevis_AddsOpportunitiesToContext()
        {
            // Arrange
            var devis = new Devis { IdDevis = 1, TypeAssurance = TypeAssurance.Auto, Montant = 1000, OwnerId = "test-user-id" };

            // Act
            await _service.CreateOpportunitiesForDevis(devis);

            // Assert
            var opportunities = await _context.Opportunities.ToListAsync();
            Assert.Equal(2, opportunities.Count); // Two opportunities should be added
            Assert.All(opportunities, o => Assert.Equal(devis.IdDevis, o.DevisId));
        }

        [Fact]
        public async Task ApproveOpportunity_UpdatesOpportunityStatus()
        {
            // Arrange
            var opportunity = new Opportunity
            {
                Id = 1,
                UserId = "test-user-id",
                Description = "Test Opportunity",
                Montant = 1000,
                DateCreation = DateTime.Now,
                AssuranceType = "Auto",
                PrimeAnnuelle = 100,
                DureeContrat = 12,
                Couverture = "Couverture de base",
                DevisId = 1,
                IsApproved = false
            };

            _context.Opportunities.Add(opportunity);
            await _context.SaveChangesAsync();

            // Act
            await _service.ApproveOpportunity(opportunity.Id, "test-user-id");

            // Assert
            var updatedOpportunity = await _context.Opportunities.FindAsync(opportunity.Id);
            Assert.True(updatedOpportunity.IsApproved);
        }


        [Fact]
        public async Task GetAllOpportunities_ReturnsAllOpportunities()
        {
            // Arrange
            _context.Opportunities.Add(new Opportunity
            {
                Description = "Opportunity 1",
                UserId = "test-user-id",
                Montant = 1000,
                DateCreation = DateTime.Now,
                AssuranceType = "Auto",
                PrimeAnnuelle = 100,
                DureeContrat = 12,
                Couverture = "Couverture de base",
                DevisId = 1
            });

            _context.Opportunities.Add(new Opportunity
            {
                Description = "Opportunity 2",
                UserId = "test-user-id",
                Montant = 2000,
                DateCreation = DateTime.Now,
                AssuranceType = "Sante",
                PrimeAnnuelle = 200,
                DureeContrat = 24,
                Couverture = "Couverture complète",
                DevisId = 2
            });

            await _context.SaveChangesAsync();

            // Act
            var result = await _service.GetAllOpportunities();

            // Assert
            Assert.Equal(2, result.Count());
        }

    }
}
