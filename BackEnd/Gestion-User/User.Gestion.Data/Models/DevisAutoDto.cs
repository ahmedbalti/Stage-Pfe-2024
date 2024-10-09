#pragma warning disable CS8618 // Suppress "Non-nullable property must contain a non-null value" warnings


using System;

namespace User.Gestion.Data.Models
{
    public class DevisAutoDto : DevisDto
    {
        public string NumeroImmatriculation { get; set; }
        public int NombreDeChevaux { get; set; }
        public int AgeVoiture { get; set; }
        public string Carburant { get; set; }
    }
}
#pragma warning restore CS8618
