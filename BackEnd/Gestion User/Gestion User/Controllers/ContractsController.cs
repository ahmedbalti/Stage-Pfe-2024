using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;
using User.Gestion.Service.Services;
using User.Gestion.Data.Models;
using System.IO;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using iText.IO.Image;
using iText.Kernel.Colors;
using iText.Kernel.Events;
using iText.Kernel.Geom;
using iText.Kernel.Pdf.Canvas;
using iText.Kernel.Colors;
using iText.Kernel.Geom;
using iText.Layout.Borders;
namespace Gestion_User.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ContractsController : ControllerBase
    {
        private readonly IContractService _contractService;
        private readonly IUserManagement _userManagement;


        public ContractsController(IContractService contractService, IUserManagement userManagement)
        {
            _contractService = contractService;
            _userManagement = userManagement;

        }

        private string GetUserId()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            return userIdClaim?.Value;
        }

        [Authorize(Roles = "Client")]
        [HttpGet]
        public async Task<IActionResult> GetContracts()
        {
            string userId = GetUserId();

            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized("User ID not found or empty.");
            }

            var contracts = await _contractService.GetContractsByUserIdAsync(userId);
            return Ok(contracts);
        }

        [Authorize(Roles = "User")]
        [HttpPost("renew")]
        public async Task<IActionResult> RenewContract([FromBody] RenewalRequest request)
        {
            string userId = GetUserId();

            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized("User ID not found or empty.");
            }

            var success = await _contractService.RenewContractAsync(request.ContractId);
            if (!success)
            {
                return BadRequest("Unable to renew contract.");
            }
            return Ok("Contract renewed successfully.");
        }
        [Authorize(Roles = "User")]
        [HttpPost]
        public async Task<IActionResult> AddContract([FromBody] ContractCreateDto newContractDto)
        {
            string userId = GetUserId();

            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized("User ID not found or empty.");
            }

            if (string.IsNullOrEmpty(newContractDto.ClientId))
            {
                return BadRequest("Client ID is required.");
            }

            var newContract = new Contract
            {
                PolicyNumber = newContractDto.PolicyNumber,
                StartDate = newContractDto.StartDate,
                EndDate = newContractDto.EndDate,
                IsActive = newContractDto.IsActive,
                UserId = userId,           // Set the UserId from the token
                ClientId = newContractDto.ClientId  // Set the ClientId from the DTO
            };

            var addedContract = await _contractService.AddContractAsync(newContract);

            if (addedContract == null)
            {
                return BadRequest("Failed to add contract.");
            }

            return Ok(addedContract);
        }


        [Authorize(Roles = "User")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateContract(int id, [FromBody] ContractCreateDto updatedContractDto)
        {
            string userId = GetUserId();

            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized("User ID not found or empty.");
            }

            var existingContract = await _contractService.GetContractByIdAsync(id);

            if (existingContract == null)
            {
                return NotFound("Contract not found.");
            }

            // Update contract properties
            existingContract.PolicyNumber = updatedContractDto.PolicyNumber;
            existingContract.StartDate = updatedContractDto.StartDate;
            existingContract.EndDate = updatedContractDto.EndDate;
            existingContract.IsActive = updatedContractDto.IsActive;

            // Update the ClientId if it's provided in the request
            if (!string.IsNullOrEmpty(updatedContractDto.ClientId))
            {
                var clientExists = await _userManagement.GetClientsAsync();
                if (clientExists.Any(c => c.Id == updatedContractDto.ClientId))
                {
                    existingContract.ClientId = updatedContractDto.ClientId;
                }
                else
                {
                    return BadRequest("Invalid Client ID.");
                }
            }

            var success = await _contractService.UpdateContractAsync(existingContract);

            if (!success)
            {
                return BadRequest("Failed to update contract.");
            }

            return Ok("Contract updated successfully.");
        }



        [Authorize(Roles = "User")]
        [HttpGet("all")]
        public async Task<IActionResult> GetAllContracts()
        {
            var contracts = await _contractService.GetAllContractsAsync();
            return Ok(contracts);
        }

        [Authorize(Roles = "User")]
        [HttpGet("clients")]
        public async Task<IActionResult> GetClients()
        {
            var clients = await _userManagement.GetClientsAsync();
            var clientList = clients.Select(c => new { c.Id, c.UserName }).ToList();
            return Ok(clientList); // This should now work correctly
        }

        [HttpGet("pdf/all")]
        public async Task<IActionResult> GenerateAllContractsPdf()
        {
            string userId = GetUserId();

            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized("User ID not found or empty.");
            }

            var contracts = await _contractService.GetContractsByUserIdAsync(userId);

            if (contracts == null || !contracts.Any())
            {
                return NotFound("No contracts found.");
            }

            using (var stream = new MemoryStream())
            {
                var writer = new PdfWriter(stream);
                var pdf = new PdfDocument(writer);
                var document = new Document(pdf, PageSize.A4);

                // Ajouter les marges personnalisées
                document.SetMargins(60, 60, 50, 50);

                // Ajouter des bordures de page
                var border = new SolidBorder(ColorConstants.BLACK, 2);
                pdf.AddEventHandler(PdfDocumentEvent.END_PAGE, new PageBorderEvent(border));

                // Ajouter le logo avec une taille plus grande
                var logo = ImageDataFactory.Create("C:\\Users\\Ahmed\\Desktop\\Stage PFE 2024\\Projet .net\\BackEnd\\Gestion User/inetum.jpg");
                var logoImage = new Image(logo).ScaleAbsolute(150, 75).SetHorizontalAlignment(HorizontalAlignment.CENTER);
                document.Add(logoImage);

                // Ajouter le titre avec une couleur personnalisée
                var darkBlueColor = new DeviceRgb(0, 0, 139); // Couleur bleu foncé
                var title = new Paragraph("Tous les contrats d'assurance")
                    .SetTextAlignment(TextAlignment.CENTER)
                    .SetFontSize(24)
                    .SetBold()
                    .SetFontColor(darkBlueColor);
                document.Add(title);

                // Ajouter les détails des contrats
                foreach (var contract in contracts)
                {
                    document.Add(new Paragraph($"Numéro de police: {contract.PolicyNumber}")
                        .SetFontSize(18)
                        .SetBold()
                        .SetFontColor(darkBlueColor));
                    document.Add(new Paragraph($"Date de début: {contract.StartDate:dd/MM/yyyy}")
                        .SetFontSize(12)
                        .SetFontColor(ColorConstants.BLACK));
                    document.Add(new Paragraph($"Date de fin: {contract.EndDate:dd/MM/yyyy}")
                        .SetFontSize(12)
                        .SetFontColor(ColorConstants.BLACK));
                    document.Add(new Paragraph($"Statut: {(contract.IsActive ? "Actif" : "Inactif")}")
                        .SetFontSize(12)
                        .SetFontColor(ColorConstants.BLACK)
                        .SetMarginBottom(20));
                }

                document.Close();

                var content = stream.ToArray();
                return File(content, "application/pdf", "AllContracts.pdf");
            }
        }
    }

  


    public class PageBorderEvent : IEventHandler
    {
        private readonly Border _border;

        public PageBorderEvent(Border border)
        {
            _border = border;
        }

        public void HandleEvent(Event @event)
        {
            PdfDocumentEvent docEvent = (PdfDocumentEvent)@event;
            PdfPage page = docEvent.GetPage();
            PdfCanvas canvas = new PdfCanvas(page);
            Rectangle rect = page.GetPageSize();
            canvas.SaveState()
                .SetLineWidth(_border.GetWidth())
                .SetStrokeColor(_border.GetColor())
                .MoveTo(rect.GetLeft(), rect.GetBottom())
                .LineTo(rect.GetRight(), rect.GetBottom())
                .LineTo(rect.GetRight(), rect.GetTop())
                .LineTo(rect.GetLeft(), rect.GetTop())
                .ClosePathStroke()
                .RestoreState();
        }

    }
    
}