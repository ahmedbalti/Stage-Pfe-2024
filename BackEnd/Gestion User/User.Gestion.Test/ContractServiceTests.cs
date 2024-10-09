using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using User.Gestion.Data.Models;
using User.Gestion.Service.Services;
using Xunit;

public class ContractServiceTests
{
    private readonly ApplicationDbContext _context;
    private readonly ContractService _service;

    public ContractServiceTests()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
        .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()) // Unique database name
        .Options;

        _context = new ApplicationDbContext(options);
        _service = new ContractService(_context);
    }

    [Fact]
    public async Task GetContractsByUserIdAsync_ReturnsContractsForUser()
    {
        // Arrange
        var userId = "test-user-id";
        _context.Contracts.Add(new Contract { UserId = userId, PolicyNumber = "12345", ClientId = "client-id" });
        _context.Contracts.Add(new Contract { UserId = userId, PolicyNumber = "67890", ClientId = "client-id" });
        _context.SaveChanges();

        // Act
        var result = await _service.GetContractsByUserIdAsync(userId);

        // Assert
        Assert.Equal(2, result.Count());
        Assert.All(result, c => Assert.Equal(userId, c.UserId));
    }

    [Fact]
    public async Task AddContractAsync_AddsContractToContext()
    {
        // Arrange
        var newContract = new Contract { PolicyNumber = "12345", UserId = "test-user-id", ClientId = "client-id" };

        // Act
        var result = await _service.AddContractAsync(newContract);

        // Assert
        Assert.Equal(1, _context.Contracts.Count()); // Now it should be 1 since IDs are not duplicated
        Assert.Equal("12345", result.PolicyNumber);
    }

    [Fact]
    public async Task UpdateContractAsync_UpdatesContractInContext()
    {
        // Arrange
        var contract = new Contract { PolicyNumber = "12345", UserId = "test-user-id", ClientId = "client-id" };
        _context.Contracts.Add(contract);
        await _context.SaveChangesAsync();

        // Act
        contract.PolicyNumber = "Updated12345";
        var result = await _service.UpdateContractAsync(contract);

        // Assert
        Assert.True(result);
        var updatedContract = await _context.Contracts.FindAsync(contract.Id); // Retrieve updated contract
        Assert.Equal("Updated12345", updatedContract.PolicyNumber);
    }

    [Fact]
    public async Task GetContractByIdAsync_ReturnsCorrectContract()
    {
        // Arrange
        var contract = new Contract { PolicyNumber = "12345", UserId = "test-user-id", ClientId = "client-id" };
        _context.Contracts.Add(contract);
        _context.SaveChanges();

        // Act
        var result = await _service.GetContractByIdAsync(contract.Id); // Use the auto-generated ID

        // Assert
        Assert.Equal("12345", result.PolicyNumber);
    }

    [Fact]
    public async Task GetAllContractsAsync_ReturnsAllContracts()
    {
        // Arrange
        // Add users to the database to reference in contracts
        var user = new ApplicationUser { Id = "test-user-id", UserName = "Test User" };
        var client = new ApplicationUser { Id = "client-id", UserName = "Test Client" };
        _context.Users.Add(user);
        _context.Users.Add(client);
        await _context.SaveChangesAsync();

        // Add contracts to the database
        _context.Contracts.Add(new Contract { PolicyNumber = "12345", UserId = user.Id, ClientId = client.Id });
        _context.Contracts.Add(new Contract { PolicyNumber = "67890", UserId = user.Id, ClientId = client.Id });
        await _context.SaveChangesAsync(); // Save changes to the database

        // Act
        var result = await _service.GetAllContractsAsync();

        // Assert
        Assert.Equal(2, result.Count()); // Verify that 2 contracts are retrieved
    }




    [Fact]
    public async Task RenewContractAsync_RenewsContract()
    {
        // Arrange
        var contract = new Contract { IsActive = true, EndDate = new DateTime(2024, 01, 01), PolicyNumber = "12345", UserId = "test-user-id", ClientId = "client-id" };
        _context.Contracts.Add(contract);
        await _context.SaveChangesAsync();

        // Act
        var result = await _service.RenewContractAsync(contract.Id); // Use the auto-generated ID

        // Assert
        Assert.True(result);
        var updatedContract = await _context.Contracts.FindAsync(contract.Id); // Retrieve updated contract
        Assert.Equal(new DateTime(2025, 01, 01), updatedContract.EndDate);
    }

}
