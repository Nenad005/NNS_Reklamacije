using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NNS_Reklamacije.Models
{
    public class Reklamacija
    {
        public int ID { get; set; }
        public string Kupac { get; set; } = string.Empty;
        public string Dobavljac { get; set; } = string.Empty;
        public DateTime? KupDatum { get; set; }
        public DateTime? DatSlanja { get; set; }
        public DateTime? DatPovratka { get; set; }
        public string Napomena { get; set; } = string.Empty;
        public string Razresenje { get; set; } = string.Empty;
        public DateTime Dodato { get; set; }
        public string Proizvodjac { get; set; } = string.Empty;
        public string NazivModela { get; set; } = string.Empty;
        public string OpisModela { get; set; } = string.Empty;
        public string Serijski { get; set; } = string.Empty;
        public int Prioritet { get; set; } = 1;
        public bool Zavrseno { get; set; } = false;
        public string Kontakt { get; set; } = string.Empty;
        public bool Izbrisano { get; set; } = false;
        public string Status
        {
            get
            {
                if (Zavrseno) return "zavrseno";
                if (DatPovratka is null) return "u toku";
                return "zavrseno";
            }
        }
    }
}
