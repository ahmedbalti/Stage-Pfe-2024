using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Threading.Tasks;
using User.Gestion.Data.Models;
using static System.Net.Mime.MediaTypeNames;



namespace User.Gestion.Service.Services
{
    public class ContractService : IContractService
    {
        private readonly ApplicationDbContext _context;

        public ContractService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Contract>> GetContractsByUserIdAsync(string userId)
        {
            return await _context.Contracts
                                 .Where(c => c.UserId == userId)
                                 .ToListAsync();
        }

        public async Task<bool> RenewContractAsync(int contractId)
        {
            var contract = await _context.Contracts.FindAsync(contractId);
            if (contract == null || !contract.IsActive)
            {
                return false;
            }

            // Update contract properties for renewal
            contract.StartDate = contract.EndDate;
            contract.EndDate = contract.EndDate.AddYears(1); // Renew for an additional year
            contract.IsActive = true;

            _context.Contracts.Update(contract);
            await _context.SaveChangesAsync();

            return true;
        }

       
        }
    }
    
