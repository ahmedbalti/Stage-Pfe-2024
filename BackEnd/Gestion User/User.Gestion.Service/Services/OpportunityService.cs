using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using User.Gestion.Data.Models;

namespace User.Gestion.Service.Services
{
    public class OpportunityService : IOpportunityService
    {
        private readonly ApplicationDbContext _context;

        public OpportunityService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Opportunity>> GetOpportunitiesByUserId(string userId)
        {
            return await _context.Opportunities
                                 .Where(o => o.UserId == userId)
                                 .ToListAsync();
        }

        public async Task<Opportunity> GetOpportunityById(int id)
        {
            return await _context.Opportunities.FindAsync(id);
        }

        public async Task CreateOpportunitiesForDevis(Devis devis)
        {
            var opportunities = new List<Opportunity>
            {
                new Opportunity
                {
                    Description = $"Offre Standard pour votre assurance {devis.TypeAssurance}",
                    Montant = devis.Montant,
                    DateCreation = DateTime.Now,
                    AssuranceType = devis.TypeAssurance.ToString(),
                    PrimeAnnuelle = devis.Montant * 0.1m, // Exemple de calcul
                    DureeContrat = 12,
                    Couverture = "Couverture de base",
                    DevisId = devis.IdDevis,
                    UserId = devis.OwnerId,
                    IsApproved = false
                },
                new Opportunity
                {
                    Description = $"Offre Premium pour votre assurance {devis.TypeAssurance}",
                    Montant = devis.Montant * 1.2m, // Exemple de calcul
                    DateCreation = DateTime.Now,
                    AssuranceType = devis.TypeAssurance.ToString(),
                    PrimeAnnuelle = devis.Montant * 0.15m, // Exemple de calcul
                    DureeContrat = 24,
                    Couverture = "Couverture complète",
                    DevisId = devis.IdDevis,
                    UserId = devis.OwnerId,
                    IsApproved = false
                }
            };

            _context.Opportunities.AddRange(opportunities);
            await _context.SaveChangesAsync();
        }
    

    public async Task ApproveOpportunity(int id, string userId)
        {
            var opportunity = await _context.Opportunities
                                            .Where(o => o.Id == id && o.UserId == userId)
                                            .FirstOrDefaultAsync();
            if (opportunity != null)
            {
                opportunity.IsApproved = true;
                await _context.SaveChangesAsync();
            }
        }
    }
}
