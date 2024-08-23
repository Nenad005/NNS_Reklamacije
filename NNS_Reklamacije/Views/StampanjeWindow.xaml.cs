using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Printing;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace NNS_Reklamacije.Views
{
    /// <summary>
    /// Interaction logic for StampanjeWindow.xaml
    /// </summary>
    public partial class StampanjeWindow : Window
    {
        Reklamacija _reklamacija;

        public StampanjeWindow( Reklamacija reklamacija)
        {
            InitializeComponent();

            Width = 400;
            Height = 180;

            _reklamacija = reklamacija;
            string defPrinter = string.Empty;
            try
            {
                var printService = new LocalPrintServer();
                defPrinter = printService.DefaultPrintQueue.FullName;
            }
            catch (Exception) { }

            foreach (string printer in PrinterSettings.InstalledPrinters)
            {
                stampacComboBox.Items.Add(printer);
                if (printer == defPrinter) stampacComboBox.SelectedIndex = stampacComboBox.Items.Count - 1;
            }

            plusButton.Click += PlusButton_Click;
            minutButton.Click += MinutButton_Click;

            stampajButton.Click += StampajButton_Click;
        }

        private void StampajButton_Click(object sender, RoutedEventArgs e)
        {
            //MessageBox.Show($"{stampacComboBox.SelectedItem.ToString()}");
            StampanjeService.Stampaj(_reklamacija, stampacComboBox.SelectedItem as string, Convert.ToInt16(counterTextBox.Text));
            this.Close();
        }

        private void MinutButton_Click(object sender, RoutedEventArgs e)
        {
            int _broj = int.Parse(counterTextBox.Text);
            if ( _broj < 2 ) return;
            counterTextBox.Text = ((_broj - 1).ToString());
        }

        private void PlusButton_Click(object sender, RoutedEventArgs e)
        {
            int _broj = int.Parse(counterTextBox.Text);
            if (_broj > 19) return;
            counterTextBox.Text = ( (_broj + 1).ToString() );
        }
    }
}
