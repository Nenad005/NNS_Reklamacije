using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NNS_Reklamacije.Commands
{
    public class PodesavanjaNavCommand : CommandBase
    {
        NavigationStore _navigationStore;
        ReklamacijeViewModel viewModel;
        public PodesavanjaNavCommand(NavigationStore navigationStore, ReklamacijeViewModel reklamacijeViewModel)
        {
            _navigationStore = navigationStore;
            viewModel = reklamacijeViewModel;
        }

        public override void Execute(object parameter)
        {
            _navigationStore.CurrentViewModel = new PodesavanjaViewModel(_navigationStore, viewModel.UspesnoUcitano);
        }
    }
}
