using Moq;
using Xunit;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using User.Gestion.Service.Services;
using Gestion_User.Controllers;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;

namespace User.Gestion.Test
{
    public class ContractsControllerTests
    {
        private readonly Mock<IContractService> _mockContractService;
        private readonly Mock<IUserManagement> _mockUserManagement;
        private readonly Mock<ILogger<ContractsController>> _mockLogger;
        private readonly ContractsController _controller;

        public ContractsControllerTests()
        {
            _mockContractService = new Mock<IContractService>();
            _mockUserManagement = new Mock<IUserManagement>();
            _mockLogger = new Mock<ILogger<ContractsController>>();
            _controller = new ContractsController(_mockContractService.Object, _mockUserManagement.Object, _mockLogger.Object);
        }

        [Fact]
        public async Task GetContracts_LogsWarning_WhenUserIdIsEmpty()
        {
            // Arrange
            _controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext()
            };

            // Act
            var result = await _controller.GetContracts();

            // Assert
            _mockLogger.Verify(logger =>
                logger.Log(
                    LogLevel.Warning,
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((v, t) => v.ToString().Contains("User ID not found or empty.")),
                    It.IsAny<Exception>(),
                    It.Is<Func<It.IsAnyType, Exception, string>>((v, t) => true)),
                Times.Once);
        }

        // Add more tests to verify that other log messages are written in different scenarios
    }
}
