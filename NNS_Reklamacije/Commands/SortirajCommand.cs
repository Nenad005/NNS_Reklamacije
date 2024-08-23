


namespace NNS_Reklamacije.Commands
{
    public class SortirajCommand : CommandBase
    {
        private string lastProp;
        public SortirajCommand()
        {
            lastProp = "ID";
        }
        public override void Execute(object parameters)
        {
            if (parameters == null) return;
            Tuple<object, object> tuple;
            ListView listView;
            string prop;
            try
            {
                tuple = (Tuple<object, object>)parameters;
                listView = (ListView)tuple.Item1;
                prop = (string)tuple.Item2;
            }
            catch (Exception)
            {
                MessageBox.Show("Došlo je do grške pri sortiranju !");
                return;
            }

            bool _flip = prop == lastProp ? true : false;

            ListSortDirection direkcija = listView.Items.SortDescriptions[0].Direction;

            listView.Items.SortDescriptions[0] = new SortDescription(prop, _flip ? flip(direkcija) : ListSortDirection.Ascending);

            lastProp = prop;
        }

        private ListSortDirection flip(ListSortDirection direkcija)
        {
            if (direkcija == ListSortDirection.Ascending) return ListSortDirection.Descending;
            return ListSortDirection.Ascending;
        }
    }
}
