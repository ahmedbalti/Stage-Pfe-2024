using System;

namespace User.Gestion.Data.Models
{
    public class DevisVieDto : DevisDto
    {
        public string Beneficiaire { get; set; }
        public int Duree { get; set; }
        public decimal Capital { get; set; }
    }
}
