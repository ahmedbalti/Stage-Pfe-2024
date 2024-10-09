using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using User.Gestion.Data.Models;

namespace User.Gestion.Service.Services
{
    public class DevisService : IDevisService
    {
        private readonly ApplicationDbContext _context;
        private readonly IOpportunityService _opportunityService;


        public DevisService(ApplicationDbContext context, IOpportunityService opportunityService)
        {
            _context = context;
            _opportunityService = opportunityService;
        }

        public async Task<IEnumerable<Devis>> GetDevisByOwnerId(string ownerId)
        {
            return await _context.Devis.Where(d => d.OwnerId == ownerId).ToListAsync();
        }

        public async Task<Devis> GetDevisByIdAndOwnerId(int id, string ownerId)
        {
            return await _context.Devis.FirstOrDefaultAsync(d => d.IdDevis == id && d.OwnerId == ownerId);
        }

        //public async Task<Devis> CreateDevis(Devis devis)
        //{
        //    _context.Devis.Add(devis);
        //    await _context.SaveChangesAsync();

        //    await _opportunityService.CreateOpportunitiesForDevis(devis);
        //    return devis;
        //}

        public async Task<Devis> CreateDevis(Devis devis)
        {
            if (devis is DevisSante devisSante)
            {
                var existingSante = await _context.Devis.OfType<DevisSante>().FirstOrDefaultAsync(d => d.NumeroSecuriteSociale == devisSante.NumeroSecuriteSociale);
                if (existingSante != null)
                {
                    throw new Exception("Numéro de sécurité sociale déjà existant.");
                }
            }
            else if (devis is DevisHabitation devisHabitation)
            {
                var existingHabitation = await _context.Devis.OfType<DevisHabitation>().FirstOrDefaultAsync(d => d.Adresse == devisHabitation.Adresse);
                if (existingHabitation != null)
                {
                    throw new Exception("Adresse déjà existante.");
                }
            }

            _context.Devis.Add(devis);
            await _context.SaveChangesAsync();

            await _opportunityService.CreateOpportunitiesForDevis(devis);
            return devis;
        }

        public async Task UpdateDevis(int id, Devis devis)
        {
            _context.Entry(devis).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteDevis(int id)
        {
            var devis = await _context.Devis.FindAsync(id);
            if (devis != null)
            {
                _context.Devis.Remove(devis);
                await _context.SaveChangesAsync();
            }
        }

        public decimal CalculerMontant(Devis devis)
        {
            decimal montant = 100; // Montant de base

            switch (devis)
            {
                case DevisAuto devisAuto:
                    montant += devisAuto.NombreDeChevaux * 10; // Exemple: 10 unités par cheval
                    montant += devisAuto.Carburant == "Essence" ? 50 : 30; // Essence coûte plus cher que Diesel
                    montant -= devisAuto.AgeVoiture * 2; // Réduction pour l'âge de la voiture
                    break;
                case DevisSante devisSante:
                    montant += 200; // Base pour l'assurance santé
                    montant += devisSante.Age > 50 ? 100 : 0; // Augmentation pour les âges > 50
                    montant += devisSante.Sexe == "Feminin" ? 50 : 0; // Exemple: Surcoût pour les femmes
                    montant += devisSante.Fumeur ? 150 : 0; // Surcoût pour les fumeurs
                    break;
                case DevisHabitation devisHabitation:
                    montant += devisHabitation.Surface * 1.5m; // Exemple: 1.5 unités par mètre carré
                    montant += devisHabitation.NombreDePieces * 20; // Exemple: 20 unités par pièce
                    break;
                case DevisVie devisVie:
                    montant += devisVie.Duree * 100; // Exemple: 100 unités par année de durée
                    montant += devisVie.Capital * 0.01m; // Exemple: 1% du capital assuré
                    break;
                default:
                    break;
            }

            return montant;
        }
    }
}
