using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NNS_Reklamacije.Commands
{
    public class UnesiSnimiCommand : CommandBase
    {
        UnosReklamacijeViewModel _viewModel;
        public UnesiSnimiCommand(UnosReklamacijeViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public override void Execute(object parameter)
        {
            switch (_viewModel.Izmena)
            {
                case true:
                    Izmeni();
                    break;

                case false:
                    Unesi();
                    break;

                default:
                    break;
            }
        }

        private void Unesi()
        {
            SQLDataGet sql = new SQLDataGet();
            Reklamacija curr = new Reklamacija()
            {
                ID = _viewModel.ID,
                Kupac = _viewModel.Kupac,
                Dobavljac = _viewModel.Dobavljac,
                KupDatum = _viewModel.KupDatum,
                DatSlanja = _viewModel.DatSlanja,
                DatPovratka = _viewModel.DatPovratka,
                Napomena = _viewModel.Napomena,
                Razresenje = _viewModel.Razresenje,
                Dodato = DateTime.Now,
                Proizvodjac = _viewModel.Proizvodjac,
                NazivModela = _viewModel.NazivModela,
                OpisModela = _viewModel.OpisModela,
                Prioritet = _viewModel.PrioritetIndex,
                Serijski = _viewModel.Serijski,
                Kontakt = _viewModel.Kontakt,
                Zavrseno = _viewModel.Zavrseno
            };

            List<string> serijskiL = sql.GetStringByProp("Serijski");
            if (serijskiL.Contains(curr.Serijski, StringComparer.OrdinalIgnoreCase))
            {
                string poruka = "Reklamacija sa istim serijsk brojem već postoji !!!" + Environment.NewLine + "Da li želiš da je ipak uneseš ?";
                string naslov = "Potvrda";
                MessageBoxButton buttons = MessageBoxButton.OKCancel;

                if (MessageBox.Show(poruka, naslov, MessageBoxButton.OKCancel) == MessageBoxResult.Cancel)
                {
                    return;
                }
            }

            sql.UnesiReklamaciju(curr);

            _viewModel.Ponisti.Execute(null);

            //MessageBox.Show("Reklamacija uspešno uneta !");
        }

        private void Izmeni()
        {
            SQLDataGet sql = new SQLDataGet();
            Reklamacija curr = new Reklamacija()
            {
                ID = _viewModel.ID,
                Kupac = _viewModel.Kupac,
                Dobavljac = _viewModel.Dobavljac,
                KupDatum = _viewModel.KupDatum,
                DatSlanja = _viewModel.DatSlanja,
                DatPovratka = _viewModel.DatPovratka,
                Napomena = _viewModel.Napomena,
                Razresenje = _viewModel.Razresenje,
                Dodato = _viewModel.Dodato,
                Proizvodjac = _viewModel.Proizvodjac,
                NazivModela = _viewModel.NazivModela,
                OpisModela = _viewModel.OpisModela,
                Prioritet = _viewModel.PrioritetIndex,
                Serijski = _viewModel.Serijski,
                Kontakt = _viewModel.Kontakt,
                Zavrseno = _viewModel.Zavrseno,
            };

            List<string> serijskiL = sql.GetStringByProp("Serijski", $"not ID={curr.ID}");
            if (serijskiL.Contains(curr.Serijski, StringComparer.OrdinalIgnoreCase))
            {
                string poruka = "Reklamacija sa istim serijsk brojem već postoji !!!" + Environment.NewLine + "Da li želiš da je ipak izmeniš ?";
                string naslov = "Potvrda";
                MessageBoxButton buttons = MessageBoxButton.OKCancel;

                if (MessageBox.Show(poruka, naslov, MessageBoxButton.OKCancel) == MessageBoxResult.Cancel)
                {
                    return;
                }
            }

            sql.IzmeniReklamaciju(curr);

            _viewModel.Ponisti.Execute(null);
        }
    }
}
