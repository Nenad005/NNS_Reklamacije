using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Data;
using NNS_Reklamacije.Commands;
using NNS_Reklamacije.Stores;

namespace NNS_Reklamacije.ViewModels
{
    public class ReklamacijeViewModel : ViewModelBase
    {
        public ICommand UnesiReklamaciju { get; }
        public ICommand Sortiraj { get; }
        public ICommand Filtritraj { get; }
        public ICommand GetData { get; }
        public ICommand Izbrisi { get; }
        public ICommand Podesavanja { get; }

        private readonly ObservableCollection<Reklamacija> _reklamacije;

        public ObservableCollection<Reklamacija> Reklamacije => _reklamacije;
        public ICollectionView ReklamacijeCollectionView { get; }

        public ReklamacijeViewModel(NavigationStore navigationStore)
        {
            _reklamacije = new ObservableCollection<Reklamacija>();

            ReklamacijeCollectionView = CollectionViewSource.GetDefaultView(Reklamacije);

            Sortiraj = new SortirajCommand();
            UnesiReklamaciju = new UnesiReklamacijuCommand(navigationStore);
            Filtritraj = new FiltrirajCommand(ReklamacijeCollectionView);
            Podesavanja = new PodesavanjaNavCommand(navigationStore, this);
            GetData = new NabaviReklamacijeAsync(this);
            Izbrisi = new IzbrisiReklamacijuCommand(_reklamacije);

            _statusSelectedIndex = 0;
            _dodatoSelectedIndex = 0;
            _prioritetSelectedIndex = 0;


            IsLoading = false;
            UspesnoUcitano = true;
        }

        private bool _uspesnoUcitano = true;
        public bool UspesnoUcitano
        {
            get
            {
                return _uspesnoUcitano;
            }
            set
            {
                _uspesnoUcitano = value;
                OnPropertyChanged(nameof(UspesnoUcitano));
            }
        }

        private bool _isLoading = false;
        public bool IsLoading
        {
            get
            {
                return _isLoading;
            }
            set
            {
                _isLoading = value;
                OnPropertyChanged(nameof(IsLoading));
            }
        }

        private int _statusSelectedIndex;
        public int StatusSelectedIndex
        {
            get
            {
                return _statusSelectedIndex;
            }
            set
            {
                _statusSelectedIndex = value;
                OnPropertyChanged(nameof(StatusSelectedIndex));
            }
        }

        private int _dodatoSelectedIndex;
        public int DodatoSelectedIndex
        {
            get
            {
                return _dodatoSelectedIndex;
            }
            set
            {
                if (_dodatoSelectedIndex == value) return;
                _dodatoSelectedIndex = value;
                OnPropertyChanged(nameof(DodatoSelectedIndex));
            }
        }

        private int _prioritetSelectedIndex;
        public int PrioritetSelectedIndex
        {
            get
            {
                return _prioritetSelectedIndex;
            }
            set
            {
                _prioritetSelectedIndex = value;
                OnPropertyChanged(nameof(PrioritetSelectedIndex));
            }
        }

        private static Tuple<DateTime,DateTime> RandomDatum()
        {
            Random rnd = new Random();
            DateTime Datum1 = new DateTime(2022, 7, rnd.Next(1,15), rnd.Next(0,24), rnd.Next(0,60), rnd.Next(0,60));
            DateTime Datum2 = new DateTime(2022, 7, rnd.Next(16,28), rnd.Next(0,24), rnd.Next(0,60), rnd.Next(0,60));
            return Tuple.Create(Datum1, Datum2);
        }
    }
}
