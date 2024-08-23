using NNS_Reklamacije.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NNS_Reklamacije.Commands
{
    public class NabaviReklamacijeCommand : CommandBase
    {
        ObservableCollection<Reklamacija> _reklamacije;

        public NabaviReklamacijeCommand(ObservableCollection<Reklamacija> reklamacije)
        {
            _reklamacije = reklamacije;
        }

        public override async void Execute(object parameter)
        {
            SQLDataGet sql = new SQLDataGet();

            var reklmacije = await sql.NapuniReklamacije();
            if (reklmacije is null) MessageBox.Show("greska");
            else
            {
                foreach (Reklamacija item in reklmacije)
                {
                    _reklamacije.Add(item);
                }
            }
        }
    }
}
