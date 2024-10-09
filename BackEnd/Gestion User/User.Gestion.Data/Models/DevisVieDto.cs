#pragma warning disable CS8618 // Suppress "Non-nullable property must contain a non-null value" warnings


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
#pragma warning restore CS8618
