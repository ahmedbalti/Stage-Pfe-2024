using System.Collections.Generic;
using System.Threading.Tasks;
using User.Gestion.Data.Models;

namespace User.Gestion.Service.Services
{
    public interface ISinistreService
    {
        Task<Sinistre> CreateSinistre(Sinistre sinistre);
        Task<Sinistre> UpdateSinistre(Sinistre sinistre);
        Task<Sinistre> GetSinistreById(int id, string userId);
        Task<List<Sinistre>> GetSinistresByUser(string userId);
    }
}
