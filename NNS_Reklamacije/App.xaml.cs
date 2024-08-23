using NNS_Reklamacije.Stores;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using static NNS_Reklamacije.Services.NovaReklamacija;

namespace NNS_Reklamacije
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly NavigationStore _navigationStore;

        public App()
        {
            _navigationStore = new NavigationStore();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            if (!PodesavanjaServices.FajlPostoji())
            { 
                PodesavanjaServices.NapraviFajl();
                _navigationStore.CurrentViewModel = new PodesavanjaViewModel(_navigationStore, null);
            }
            else _navigationStore.CurrentViewModel = new ReklamacijeViewModel(_navigationStore);

            MainWindow = new MainWindow()
            {
                DataContext = new MainWindowViewModel(_navigationStore)
            };
            MainWindow.Show();

            base.OnStartup(e);
        }
    }
}
