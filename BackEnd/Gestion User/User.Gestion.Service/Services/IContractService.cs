using System.Collections.Generic;
using System.Threading.Tasks;
using User.Gestion.Data.Models;

namespace User.Gestion.Service.Services
{
    public interface IContractService
    {
        Task<IEnumerable<Contract>> GetContractsByUserIdAsync(string userId);
        Task<bool> RenewContractAsync(int contractId);

        Task<Contract> AddContractAsync(Contract newContract);


        Task<bool> UpdateContractAsync(Contract contract);
        Task<Contract> GetContractByIdAsync(int id);

        Task<IEnumerable<Contract>> GetAllContractsAsync();


    }
}
