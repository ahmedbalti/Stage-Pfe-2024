using System;

namespace User.Gestion.Data.Models
{
    public class DevisHabitationDto : DevisDto
    {
        public string Adresse { get; set; }
        public int Surface { get; set; }
        public int NombreDePieces { get; set; }
    }
}
