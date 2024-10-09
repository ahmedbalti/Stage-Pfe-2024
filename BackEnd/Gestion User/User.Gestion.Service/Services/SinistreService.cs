using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using User.Gestion.Data;
using User.Gestion.Data.Models;

namespace User.Gestion.Service.Services
{
    public class SinistreService : ISinistreService
    {
        private readonly ApplicationDbContext _context;

        public SinistreService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Sinistre> CreateSinistre(Sinistre sinistre)
        {
            _context.Sinistres.Add(sinistre);
            await _context.SaveChangesAsync();
            return sinistre;
        }

        public async Task<Sinistre> UpdateSinistre(Sinistre sinistre)
        {
            _context.Sinistres.Update(sinistre);
            await _context.SaveChangesAsync();
            return sinistre;
        }

        public async Task<Sinistre> GetSinistreById(int id, string userId)
        {
            return await _context.Sinistres
                .FirstOrDefaultAsync(s => s.Id == id && s.UserId == userId);
        }

        public async Task<List<Sinistre>> GetSinistresByUser(string userId)
        {
            return await _context.Sinistres
                .Where(s => s.UserId == userId)
                .ToListAsync();
        }
        public async Task<List<Sinistre>> GetAllSinistres()
        {
            return await _context.Sinistres.ToListAsync();
        }
    }
}
