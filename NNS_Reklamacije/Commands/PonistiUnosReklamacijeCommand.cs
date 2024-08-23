


namespace NNS_Reklamacije.Commands
{
    public class PonistiUnosReklamacijeCommand : CommandBase
    {
        private readonly NavigationStore _navigationStore;

        public PonistiUnosReklamacijeCommand(NavigationStore navigationStore)
        {
            _navigationStore = navigationStore;
        }


        public override void Execute(object parameter)
        {
            _navigationStore.CurrentViewModel = new ReklamacijeViewModel(_navigationStore);
        }
    }
}
