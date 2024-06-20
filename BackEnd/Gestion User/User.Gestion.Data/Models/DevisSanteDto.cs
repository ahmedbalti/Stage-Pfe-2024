using System;

namespace User.Gestion.Data.Models
{
    public class DevisSanteDto : DevisDto
    {
        public string NumeroSecuriteSociale { get; set; }
        public int Age { get; set; }
        public string Sexe { get; set; }
        public bool Fumeur { get; set; }
    }
}
