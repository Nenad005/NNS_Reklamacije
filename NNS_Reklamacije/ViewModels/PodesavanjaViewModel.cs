using NNS_Reklamacije.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NNS_Reklamacije.ViewModels
{
    public class PodesavanjaViewModel : ViewModelBase
    {

        //public PodesavanjaViewModel(NavigationStore navigationStore)
        //{
        //    Nazad = new PonistiUnosReklamacijeCommand(navigationStore);
        //    Sacuvaj = new SacuvajPodesavanjaCommand(this);
        //    Podeseno = true;
        //}
        public PodesavanjaViewModel(NavigationStore navigationStore, bool uspesnoucitano)
        {
            Postoji = uspesnoucitano;

            Nazad = new PonistiUnosReklamacijeCommand(navigationStore);
            Sacuvaj = new SacuvajPodesavanjaCommand(this);
            Podeseno = true;
        }

        public PodesavanjaViewModel(NavigationStore navigationStore, object bla)
        {
            Postoji = true;

            Nazad = new PonistiUnosReklamacijeCommand(navigationStore);
            Sacuvaj = new SacuvajPodesavanjaCommand(this);
            Podeseno = false;
        }

        public bool Postoji { get; }

        private string _imeServera;
        public string ImeServera
        {
            get
            {
                return _imeServera;
            }
            set
            {
                _imeServera = value;
                OnPropertyChanged(nameof(ImeServera));
            }
        }

        private int _sqlAutentifikacija;
        public int SqlAutentifikacija
        {
            get
            {
                return _sqlAutentifikacija;
            }
            set
            {
                _sqlAutentifikacija = value;
                OnPropertyChanged(nameof(SqlAutentifikacija));
            }
        }

        private string _userName;
        public string UserName
        {
            get
            {
                return _userName;
            }
            set
            {
                _userName = value;
                OnPropertyChanged(nameof(UserName));
            }
        }

        private string _password;
        public string Password
        {
            get
            {
                return _password;
            }
            set
            {
                _password = value;
                OnPropertyChanged(nameof(Password));
            }
        }

        private string _imeBaze;
        public string ImeBaze
        {
            get
            {
                return _imeBaze;
            }
            set
            {
                _imeBaze = value;
                OnPropertyChanged(nameof(ImeBaze));
            }
        }

        private string _imeFirme;
        public string ImeFirme
        {
            get
            {
                return _imeFirme;
            }
            set
            {
                _imeFirme = value;
                OnPropertyChanged(nameof(ImeFirme));
            }
        }

        private string _pib;
        public string PIB
        {
            get
            {
                return _pib;
            }
            set
            {
                _pib = value;
                OnPropertyChanged(nameof(PIB));
            }
        }

        private string _adresa;
        public string Adresa
        {
            get
            {
                return _adresa;
            }
            set
            {
                _adresa = value;
                OnPropertyChanged(nameof(Adresa));
            }
        }

        private string _brojTelefona;
        public string BrojTelefona
        {
            get
            {
                return _brojTelefona;
            }
            set
            {
                _brojTelefona = value;
                OnPropertyChanged(nameof(BrojTelefona));
            }
        }

        private string _eMail;
        public string EMail
        {
            get
            {
                return _eMail;
            }
            set
            {
                _eMail = value;
                OnPropertyChanged(nameof(EMail));
            }
        }

        private string _postanskiBroj;
        public string PostanskiBroj
        {
            get
            {
                return _postanskiBroj;
            }
            set
            {
                _postanskiBroj = value;
                OnPropertyChanged(nameof(PostanskiBroj));
            }
        }

        private string _maticniBroj;
        public string MaticniBroj
        {
            get
            {
                return _maticniBroj;
            }
            set
            {
                _maticniBroj = value;
                OnPropertyChanged(nameof(MaticniBroj));
            }
        }

        public bool Podeseno { get; }

        public ICommand Nazad { get; }
        public ICommand Sacuvaj { get; }
    }
}
