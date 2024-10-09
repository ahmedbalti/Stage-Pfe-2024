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

        public async Task<Contract> AddContractAsync(Contract newContract)
        {
            _context.Contracts.Add(newContract);
            await _context.SaveChangesAsync();
            return newContract;
        }



        public async Task<bool> UpdateContractAsync(Contract contract)
        {
            _context.Contracts.Update(contract);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<Contract> GetContractByIdAsync(int id)
        {
            return await _context.Contracts.FindAsync(id);
        }
        //public async Task<IEnumerable<Contract>> GetAllContractsAsync()
        //{
        //    return await _context.Contracts.ToListAsync();
        //}

        public async Task<IEnumerable<Contract>> GetAllContractsAsync()
        {
            return await _context.Contracts
                .Include(c => c.ApplicationUser)  // Include the User (Client)
                .Include(c => c.Client)  // Include the client using the ClientId
                .Select(c => new Contract
                {
                    Id = c.Id,
                    PolicyNumber = c.PolicyNumber,
                    StartDate = c.StartDate,
                    EndDate = c.EndDate,
                    IsActive = c.IsActive,
                    UserId = c.UserId,
                    ClientId = c.ClientId,
                    ClientName = c.Client.UserName  // Use the client's username
                })
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
    
