using System.Collections.Generic;
using System.Threading.Tasks;
using User.Gestion.Data.Models;

namespace User.Gestion.Service.Services
{
    public interface IOpportunityService
    {
        Task<IEnumerable<Opportunity>> GetOpportunitiesByUserId(string userId);
        Task<Opportunity> GetOpportunityById(int id);
        Task CreateOpportunitiesForDevis(Devis devis);
        Task ApproveOpportunity(int id, string userId);
        Task<IEnumerable<Opportunity>> GetAllOpportunities();

    }
}
