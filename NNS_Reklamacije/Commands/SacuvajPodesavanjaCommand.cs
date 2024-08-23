using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NNS_Reklamacije.Commands
{
    public class SacuvajPodesavanjaCommand : CommandBase
    {
        PodesavanjaViewModel _viewModel;

        public SacuvajPodesavanjaCommand(PodesavanjaViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public override void Execute(object parameter)
        {
            string _imeServera = _viewModel.ImeServera;
            bool _sqlAutentifikacija = _viewModel.SqlAutentifikacija == 0 ? true : false;
            string _userName = _viewModel.UserName;
            string _password = _viewModel.Password;
            string _imeBaze = _viewModel.ImeBaze;
            string _imeFirme = _viewModel.ImeFirme;
            string _pib = _viewModel.PIB;
            string _adresa = _viewModel.Adresa;
            string _brojTelefona = _viewModel.BrojTelefona;
            string _eMail = _viewModel.EMail;
            string _postanki = _viewModel.PostanskiBroj;
            string _maticni = _viewModel.MaticniBroj;

            Podesavanja sts = PodesavanjaServices.ProcitajFajl();

            sts.ImeServera = _imeServera;
            sts.SqlAutentifikacija = _sqlAutentifikacija;
            sts.UserName = _userName;
            sts.Password = _password;
            sts.ImeBaze = _imeBaze;
            sts.ImeFirme = _imeFirme;
            sts.PIB = _pib;
            sts.Adresa = _adresa;
            sts.BrojTelefona = _brojTelefona;
            sts.EMail = _eMail;
            sts.Postanskibroj = _postanki;
            sts.MaticniBroj = _maticni;

            if (!sts.Serveri.Contains(_imeServera)) sts.Serveri.Add(_imeServera);
            if (!sts.UserNames.Contains(_userName)) sts.UserNames.Add(_userName);

            PodesavanjaServices.Sacuvaj(sts);

            _viewModel.Nazad.Execute(null);
        }
    }
}
