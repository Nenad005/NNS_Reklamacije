using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace NNS_Reklamacije.Views
{
    /// <summary>
    /// Interaction logic for UnosReklamacijeView.xaml
    /// </summary>
    public partial class UnosReklamacijeView : UserControl
    {
        public UnosReklamacijeView()
        {
            InitializeComponent();

            prioritetComboBox.Items.Add("NIZAK");
            prioritetComboBox.Items.Add("NORMALAN");
            prioritetComboBox.Items.Add("VISOK");

            prioritetComboBox.SelectedIndex = 1;

            kupacSuggestions.Loaded += KupacSuggestions_Loaded;
            dobavljacSuggestions.Loaded += DobavljacSuggestions_Loaded;
            proizvodjacSuggestions.Loaded += ProizvodjacSuggestions_Loaded;
            kontaktSuggestions.autoTextBox.GotFocus += KontaktAutoTextBox_GotFocus;
            modelSuggestions.autoTextBox.GotFocus += ModelAutoTextBox_GotFocus;
            opisSuggestions.autoTextBox.GotFocus += OpisAutoTextBox_GotFocus;
        }

        private void OpisAutoTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            SQLDataGet sql = new SQLDataGet();
            opisSuggestions.AutoSuggestionList = sql.GetStringByProp("OpisModela", $"NazivModela='{modelSuggestions.Text}'");
        }

        private void ModelAutoTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            SQLDataGet sql = new SQLDataGet();
            modelSuggestions.AutoSuggestionList = sql.GetStringByProp("NazivModela", $"Proizvodjac='{proizvodjacSuggestions.Text}'");
        }

        private void KontaktAutoTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            SQLDataGet sql = new SQLDataGet();
            kontaktSuggestions.AutoSuggestionList = sql.GetStringByProp("Kontakt", $"Kupac='{kupacSuggestions.Text}'");
        }

        private void ProizvodjacSuggestions_Loaded(object sender, RoutedEventArgs e)
        {
            SQLDataGet sql = new SQLDataGet();
            proizvodjacSuggestions.AutoSuggestionList = sql.GetStringByProp("Proizvodjac");
        }

        private void DobavljacSuggestions_Loaded(object sender, RoutedEventArgs e)
        {
            SQLDataGet sql = new SQLDataGet();
            dobavljacSuggestions.AutoSuggestionList = sql.GetStringByProp("Dobavljac");
        }

        private void KupacSuggestions_Loaded(object sender, RoutedEventArgs e)
        {
            SQLDataGet sql = new SQLDataGet();
            kupacSuggestions.AutoSuggestionList = sql.GetStringByProp("Kupac");
        }
    }
}
