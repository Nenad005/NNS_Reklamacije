using System;
using System.Collections.Generic;
using System.IO;
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
    /// Interaction logic for PodesavanjaView.xaml
    /// </summary>
    public partial class PodesavanjaView : UserControl
    {
        Podesavanja _podesavanja;

        public PodesavanjaView()
        {
            InitializeComponent();

            _podesavanja = PodesavanjaServices.ProcitajFajl();

            NacinAutentifikacijeComboBox.Items.Add("SQl Autentifikacija");
            NacinAutentifikacijeComboBox.Items.Add("WINDOWS Autentifikacija");
            NacinAutentifikacijeComboBox.SelectedIndex = _podesavanja.SqlAutentifikacija ? 0 : 1;

            ImeServeraSuggestions.Loaded += ImeServeraSuggestions_Loaded;
            UsernameSuggestions.Loaded += UsernameSuggestions_Loaded;
            PasswordSuggestions.Loaded += PasswordSuggestions_Loaded;
            ImeBazeTextBox.Loaded += ImeBazeTextBox_Loaded;
            NacinAutentifikacijeComboBox.SelectionChanged += NacinAutentifikacijeComboBox_SelectionChanged;
            ImeFirmeTextBox.Loaded += ImeFirmeTextBox_Loaded;
            PIBTextBox.Loaded += PIBTextBox_Loaded;
            AdresaTextBox.Loaded += AdresaTextBox_Loaded;
            TelefonTextBox.Loaded += TelefonTextBox_Loaded;
            EMailTextBox.Loaded += EMailTextBox_Loaded;
            postanskiTextBox.Loaded += PostanskiTextBox_Loaded;
            maticniTextBox.Loaded += MaticniTextBox_Loaded;

            generisiButton.Click += GenerisiButton_Click;
        }

        private void GenerisiButton_Click(object sender, RoutedEventArgs e)
        {
            SQLDataGet sql = new SQLDataGet();
            sql.NapraviTabelu();
        }

        private void MaticniTextBox_Loaded(object sender, RoutedEventArgs e)
        {
            maticniTextBox.Text = _podesavanja.MaticniBroj;
        }

        private void PostanskiTextBox_Loaded(object sender, RoutedEventArgs e)
        {
            postanskiTextBox.Text = _podesavanja.Postanskibroj;
        }

        private void EMailTextBox_Loaded(object sender, RoutedEventArgs e)
        {
            EMailTextBox.Text = _podesavanja.EMail;
        }

        private void TelefonTextBox_Loaded(object sender, RoutedEventArgs e)
        {
            TelefonTextBox.Text = _podesavanja.BrojTelefona;
        }

        private void AdresaTextBox_Loaded(object sender, RoutedEventArgs e)
        {
            AdresaTextBox.Text = _podesavanja.Adresa;
        }

        private void PIBTextBox_Loaded(object sender, RoutedEventArgs e)
        {
            PIBTextBox.Text = _podesavanja.PIB;
        }

        private void ImeFirmeTextBox_Loaded(object sender, RoutedEventArgs e)
        {
            ImeFirmeTextBox.Text = _podesavanja.ImeFirme;
        }

        private void NacinAutentifikacijeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (NacinAutentifikacijeComboBox.SelectedIndex == 1)
            { 
                PasswordSuggestions.Text = "";
                UsernameSuggestions.Text = "";
            }
        }

        private void ImeBazeTextBox_Loaded(object sender, RoutedEventArgs e)
        {
            ImeBazeTextBox.Text = _podesavanja.ImeBaze;
        }

        private void PasswordSuggestions_Loaded(object sender, RoutedEventArgs e)
        {
            PasswordSuggestions.Text = _podesavanja.Password;
        }

        private void UsernameSuggestions_Loaded(object sender, RoutedEventArgs e)
        {
            UsernameSuggestions.Text = _podesavanja.UserName;
            UsernameSuggestions.AutoSuggestionList = _podesavanja.UserNames;
        }

        private void ImeServeraSuggestions_Loaded(object sender, RoutedEventArgs e)
        {
            ImeServeraSuggestions.Text = _podesavanja.ImeServera;
            ImeServeraSuggestions.AutoSuggestionList = _podesavanja.Serveri;
        }
    }
}
