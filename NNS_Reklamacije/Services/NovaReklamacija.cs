using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NNS_Reklamacije.Services
{
    public static class NovaReklamacija
    {
        public static Reklamacija NapraviRandomReklamaciju(ObservableCollection<Reklamacija> _reklamacije)
        {
            string[] imena = new string[] { "Marko", "Aleksa", "Nikola", "Petar", "Nenad", "Milos", "Vlada", "Uros", "Krisijan", "Janko", "Stefan"};
            string[] prezimena = new string[] { "Markovic", "Aleksic", "Nikolic", "Petrovic", "Nenadovic", "Milosevic", "Vladovic", "Urosevic", "Golubovic", "Jankovic", "Stefanovic", "Erceg"};
            string[] dobavljaci = new string[] { "Monitor Systems", "NNS Systems", "BC GROUP", "AXE PC", "EWE", "KimTek D.O.O",
                                                 "Noctua Cluch", "CLUCH Systems", "Atrium Gaming", "Dopex D.O.O", "DuplexPC Systems", "CT shop"};
            string[] proizvodjaci = new string[] { "DELL", "HP", "GIGABYTE", "MSI", "CHIEFTEC", "COOLER MASTER", "NOCTUA", "KINGSTON", "A-DATA", "APPLE", "XIAOMI", "HISENSE", "ASUS" };
            //string[] imena = new string[] { "Marko" };
            //string[] prezimena = new string[] { "Nenadovic" };



            Random rnd = new Random();

            string _kupac = imena[rnd.Next(imena.Length)] + " " + prezimena[rnd.Next(prezimena.Length)];
            string _dobavljac = dobavljaci[rnd.Next(dobavljaci.Length)];


            int _ID = _reklamacije.Count == 0 ? 0 : _reklamacije.OrderBy(x => x.ID).Last().ID + 1;

            Tuple<DateTime, DateTime> SlanjePovratak = RandomDatum();
            TimeSpan KupTime = new TimeSpan(rnd.Next(25, 365), rnd.Next(0, 24), rnd.Next(0, 60), rnd.Next(0, 60));

            return new Reklamacija()
            {
                ID = _ID,
                Kupac = _kupac,
                Dobavljac = _dobavljac,
                DatPovratka = rnd.Next(0, 2) == 0 ? SlanjePovratak.Item2 : null,
                DatSlanja = SlanjePovratak.Item1,
                KupDatum = rnd.Next(0, 10) == 8 ? null : DateTime.Now.Subtract(KupTime),
                Dodato = DateTime.Now.Subtract(new TimeSpan(rnd.Next(0,20), 0, 0, 0)),
                Prioritet = rnd.Next(0, 3),
                Proizvodjac = proizvodjaci[rnd.Next(0, proizvodjaci.Length)],
                NazivModela = RandomModel(),
                Serijski = RandomSerijski(rnd.Next(15, 20))
            };
        }
        
        private static Tuple<DateTime, DateTime> RandomDatum()
        {
            Random rnd = new Random();

            TimeSpan time1 = new TimeSpan(rnd.Next(5,25), rnd.Next(0,24), rnd.Next(0,60), rnd.Next(0,60));
            TimeSpan time2 = new TimeSpan(rnd.Next(0,5), rnd.Next(0,24), rnd.Next(0,60), rnd.Next(0,60));

            DateTime Datum1 = DateTime.Now.Subtract(time1);
            DateTime Datum2 = DateTime.Now.Subtract(time2);

            return Tuple.Create(Datum1, Datum2);
        }

        private static string RandomModel()
        {
            char[] slova = new char[] { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R',
                                            'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z'};

            Random rnd = new Random();
            string model = string.Empty;

            int brojslova = rnd.Next(1, 4);
            for (int i = 0; i < brojslova; i++)
            {
                model += slova[rnd.Next(0, slova.Length)].ToString();
            }

            int brojcifara = rnd.Next(3, 5);
            for (int i = 0; i < brojcifara; i++)
            {
                model += rnd.Next(0, 10).ToString();
            }

            model += rnd.Next(0, 2) == 1 ? "" : slova[rnd.Next(0, slova.Length)].ToString();

            return model;
        }

        private static string RandomSerijski(int n)
        {
            char[] slova = new char[] { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R',
                                            'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z'};

            Random rnd = new Random();

            string serijski = string.Empty;
            for (int i = 0; i < n; i++)
            {
                serijski += rnd.Next(0, 2) == 1 ? rnd.Next(0, 10) : slova[rnd.Next(0, slova.Length)].ToString();
            }

            return serijski;
        }
    }
}
