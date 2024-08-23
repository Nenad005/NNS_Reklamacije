using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NNS_Reklamacije.Services
{
    public static class PodesavanjaServices
    {
        private static string fajl => @$"{Directory.GetCurrentDirectory()}\podesavanja.json";

        public static bool FajlPostoji()
        {
            return File.Exists(fajl);
        }

        public static void NapraviFajl()
        {
            Podesavanja sts = new Podesavanja()
            {
                ImeServera = ".",
                Serveri = new List<string> { ".", "localhost" },
                SqlAutentifikacija = false,
                UserName = "",
                UserNames = new List<string>(),
                Password = "",
                ImeBaze = "model",
                ImeFirme = "ImeFirme . . .",
                PIB = "PIB . . .",
                Adresa = "Adresa . . .",
                BrojTelefona = "Broj Telefona . . .",
                EMail = "E-Mail . . .",
                Postanskibroj = "Postanski broj . . .",
                MaticniBroj = "Maticni Broj Firme  . . ."
            };

            string json = JsonConvert.SerializeObject(sts, Formatting.Indented);

            File.WriteAllText(fajl, json);
        }

        public static Podesavanja ProcitajFajl()
        {
            if (!FajlPostoji()) NapraviFajl();

            string jsonstr = File.ReadAllText(fajl);
            return JsonConvert.DeserializeObject<Podesavanja>(jsonstr);
        }

        public static void Sacuvaj(Podesavanja sts)
        {
            string json = JsonConvert.SerializeObject(sts, Formatting.Indented);
            File.WriteAllText(fajl, json);
        }
    }
    public struct Podesavanja
    {
        public string ImeServera { get; set; }
        public List<string> Serveri { get; set; }
        public bool SqlAutentifikacija { get; set; }
        public string UserName { get; set; }
        public List<string> UserNames { get; set; }
        public string Password { get; set; }
        public string ImeBaze { get; set; }
        public string ImeFirme { get; set; }
        public string PIB { get; set; }
        public string Adresa { get; set; }
        public string BrojTelefona { get; set; }
        public string EMail { get; set; }
        public string Postanskibroj { get; set; }
        public string MaticniBroj { get; set; }
    }
}
