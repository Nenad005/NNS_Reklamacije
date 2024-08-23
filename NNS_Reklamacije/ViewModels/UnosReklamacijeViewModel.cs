using NNS_Reklamacije.Commands;
using NNS_Reklamacije.Stores;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NNS_Reklamacije.ViewModels
{
    public class UnosReklamacijeViewModel : ViewModelBase
    {
        //private NavigationStore _navigationStore;

        public UnosReklamacijeViewModel(NavigationStore navigationStore)
        {
            SQLDataGet sql = new SQLDataGet();

            Ponisti = new PonistiUnosReklamacijeCommand(navigationStore);
            UnesiSnimi = new UnesiSnimiCommand(this);

            Izmena = false;

            ID = sql.GetMaxID();
        }

        public UnosReklamacijeViewModel(NavigationStore navigationStore, Reklamacija reklamacija)
        {
            Ponisti = new PonistiUnosReklamacijeCommand(navigationStore);
            UnesiSnimi = new UnesiSnimiCommand(this);
            ID = reklamacija.ID;
            Kupac = reklamacija.Kupac;
            Dobavljac = reklamacija.Dobavljac;
            KupDatum = reklamacija.KupDatum;
            DatSlanja = reklamacija.DatSlanja;
            Dodato = reklamacija.Dodato;
            DatPovratka = reklamacija.DatPovratka;
            Napomena = reklamacija.Napomena;
            Razresenje = reklamacija.Razresenje;
            Proizvodjac = reklamacija.Proizvodjac;
            NazivModela = reklamacija.NazivModela;
            OpisModela = reklamacija.OpisModela;
            Serijski = reklamacija.Serijski;
            PrioritetIndex = reklamacija.Prioritet;
            Kontakt = reklamacija.Kontakt;
            Zavrseno = reklamacija.Zavrseno;

            Izmena = true;

        }


        //<------------------------------------------------------------>\\

        public bool Izmena { get; }

        public DateTime Dodato { get; }

        public int ID { get; }

        private string _kupac;
        public string Kupac
        {
            get
            {
                return _kupac;
            }
            set
            {
                _kupac = value;
            OnPropertyChanged(nameof(Kupac));
            }
        }

        private DateTime? _kupDatum;
        public DateTime? KupDatum
        {
            get
            {
                return _kupDatum;
            }
            set
            {
                _kupDatum = value;
                OnPropertyChanged(nameof(KupDatum));
            }
        }

        private string _dobavljac;
        public string Dobavljac
        {
            get
            {
                return _dobavljac;
            }
            set
            {
                _dobavljac = value;
                OnPropertyChanged(nameof(Dobavljac));
            }
        }

        private DateTime? _datSlanja;
        public DateTime? DatSlanja
        {
            get
            {
                return _datSlanja;
            }
            set
            {
                _datSlanja = value;
                OnPropertyChanged(nameof(DatSlanja));
            }
        }

        private DateTime? _datPovratka;
        public DateTime? DatPovratka
        {
            get
            {
                return _datPovratka;
            }
            set
            {
                _datPovratka = value;
                OnPropertyChanged(nameof(DatPovratka));
            }
        }

        private string _napomena;
        public string Napomena
        {
            get
            {
                return _napomena;
            }
            set
            {
                _napomena = value;
                OnPropertyChanged(nameof(Napomena));
            }
        }

        private string _razresenje;
        public string Razresenje
        {
            get
            {
                return _razresenje;
            }
            set
            {
                _razresenje = value;
                OnPropertyChanged(nameof(Razresenje));
            }
        }

        private string _proizvodjac;
        public string Proizvodjac
        {
            get
            {
                return _proizvodjac;
            }
            set
            {
                _proizvodjac = value;
                OnPropertyChanged(nameof(Proizvodjac));
            }
        }

        private string _nazivModela;
        public string NazivModela
        {
            get
            {
                return _nazivModela;
            }
            set
            {
                _nazivModela = value;
                OnPropertyChanged(nameof(NazivModela));
            }
        }

        private string _opisModela;
        public string OpisModela
        {
            get
            {
                return _opisModela;
            }
            set
            {
                _opisModela = value;
                OnPropertyChanged(nameof(OpisModela));
            }
        }

        private string _serijski;
        public string Serijski
        {
            get
            {
                return _serijski;
            }
            set
            {
                _serijski = value;
                OnPropertyChanged(nameof(Serijski));
            }
        }

        private int _prioritetIndex = 1;
        public int PrioritetIndex
        {
            get
            {
                return _prioritetIndex;
            }
            set
            {
                _prioritetIndex = value;
                OnPropertyChanged(nameof(PrioritetIndex));
            }
        }

        private string _kontakt;
        public string Kontakt
        {
            get
            {
                return _kontakt;
            }
            set
            {
                _kontakt = value;
                OnPropertyChanged(nameof(Kontakt));
            }
        }

        private bool _zavrseno;
        public bool Zavrseno
        {
            get
            {
                return _zavrseno;
            }
            set
            {
                _zavrseno = value;
                OnPropertyChanged(nameof(Zavrseno));
            }
        }

        public ICommand UnesiSnimi { get; }
        public ICommand Ponisti { get; }
    }
}
