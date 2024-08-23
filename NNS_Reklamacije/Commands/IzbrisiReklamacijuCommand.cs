using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NNS_Reklamacije.Commands
{
    partial class IzbrisiReklamacijuCommand : CommandBase
    {
        ObservableCollection<Reklamacija> _reklamacije;
        public IzbrisiReklamacijuCommand(ObservableCollection<Reklamacija> reklamacije)
        {
            _reklamacije = reklamacije;
        }
        public override void Execute(object parameter)
        {
            try
            {
                ListView listView = parameter as ListView;
                int id = (listView.Items[listView.SelectedIndex] as Reklamacija).ID;

                string poruka = "Da li si siguran da zelis da izbrises reklamaciju ?";
                string naslov = "Potvrda";
                MessageBoxButton buttons = MessageBoxButton.OKCancel;

                if (MessageBox.Show(poruka, naslov, MessageBoxButton.OKCancel) == MessageBoxResult.OK)
                {
                    SQLDataGet sql = new SQLDataGet();
                    sql.IzbrisiReklamaciju(id);
                    _reklamacije.Remove(_reklamacije.Where(x => x.ID == id).FirstOrDefault());
                    //((ListViewItem)listView.ItemContainerGenerator.ContainerFromItem(
                    //    listView.Items[listView.SelectedIndex])).Background = Brushes.White;
                }
                else
                {
                    return;
                }

            }
            catch (Exception)
            {
                MessageBox.Show("Došlo je do grske pri brisanju reklamacije");
            }
        }
    }
}
