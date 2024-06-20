using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
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

        public async Task<IEnumerable<Sinistre>> GetSinistresByUserIdAsync(string userId)
        {
            return await _context.Sinistres.Where(s => s.UserId == userId).ToListAsync();
        }

        public async Task<Sinistre> GetSinistreByIdAsync(int id)
        {
            return await _context.Sinistres.FindAsync(id);
        }

        public async Task AddSinistreAsync(Sinistre sinistre)
        {
            _context.Sinistres.Add(sinistre);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateSinistreAsync(Sinistre sinistre)
        {
            _context.Entry(sinistre).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task<bool> SinistreExistsAsync(int id)
        {
            return await _context.Sinistres.AnyAsync(e => e.Id == id);
        }
    }
}
