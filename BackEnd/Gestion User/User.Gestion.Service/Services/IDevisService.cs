using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using User.Gestion.Data.Models;

namespace User.Gestion.Service.Services
{
    public interface IDevisService
    {
        Task<IEnumerable<Devis>> GetDevisByOwnerId(string ownerId);
        Task<Devis> GetDevisByIdAndOwnerId(int id, string ownerId);
        Task<Devis> CreateDevis(Devis devis);
        Task UpdateDevis(int id, Devis devis);
        Task DeleteDevis(int id);
        decimal CalculerMontant(Devis devis);
    }
}

