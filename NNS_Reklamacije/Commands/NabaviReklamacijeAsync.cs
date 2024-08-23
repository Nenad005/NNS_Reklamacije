using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NNS_Reklamacije.Commands
{
    public class NabaviReklamacijeAsync : AsyncCommandBase
    {
        ReklamacijeViewModel _viewModel;

        public NabaviReklamacijeAsync(ReklamacijeViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        protected override async Task ExecuteAsync(object parameter)
        {
            _viewModel.IsLoading = true;

            SQLDataGet sql = new SQLDataGet();

            var reklamacije = await sql.NapuniReklamacije();
            _viewModel.IsLoading = false;

            if (reklamacije is null) _viewModel.UspesnoUcitano = false;
            else await FillListView(reklamacije);
        }

        private Task FillListView(List<Reklamacija> reklamacije)
        {
            return Task.Factory.StartNew(() =>
            {
                foreach (Reklamacija item in reklamacije)
                {
                    App.Current.Dispatcher.BeginInvoke((Action)delegate { _viewModel.Reklamacije.Add(item); });
                    //Thread.Sleep(10);
                }
            });

            //foreach (Reklamacija item in reklamacije)
            //{
            //    _viewModel.Reklamacije.Add(item);
            //}
        }
    }
}
