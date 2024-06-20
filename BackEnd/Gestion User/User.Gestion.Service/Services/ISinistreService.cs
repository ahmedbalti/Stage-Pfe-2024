using System.Collections.Generic;
using System.Threading.Tasks;
using User.Gestion.Data.Models;

namespace User.Gestion.Service.Services
{
    public interface ISinistreService
    {
        Task<IEnumerable<Sinistre>> GetSinistresByUserIdAsync(string userId);
        Task<Sinistre> GetSinistreByIdAsync(int id);
        Task AddSinistreAsync(Sinistre sinistre);
        Task UpdateSinistreAsync(Sinistre sinistre);
        Task<bool> SinistreExistsAsync(int id);
    }
}
