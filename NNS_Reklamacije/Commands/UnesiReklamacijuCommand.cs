


namespace NNS_Reklamacije.Commands
{
    internal class UnesiReklamacijuCommand : CommandBase
    {
        private readonly NavigationStore _navigationStore;

        public UnesiReklamacijuCommand(NavigationStore navigationStore)
        {
            _navigationStore = navigationStore;
        }

        public override void Execute(object parameter)
        {
            if (parameter is null)
            {
                _navigationStore.CurrentViewModel = new UnosReklamacijeViewModel(_navigationStore);
            }
            else
            {
                ListView listView = (ListView)parameter;
                Reklamacija curr = listView.Items[listView.SelectedIndex] as Reklamacija;

                _navigationStore.CurrentViewModel = new UnosReklamacijeViewModel(_navigationStore, curr);
            }
        }
    }
}
