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
