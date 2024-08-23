


namespace NNS_Reklamacije.Commands
{
    public class FiltrirajCommand : CommandBase
    {
        private ICollectionView _data;
        private int _statusSelectedIndex = 0;
        private int _dodatoSelectedIndex = 0;
        private int _prioritetSelectedIndex = 0;
        private int _searchCatID = 0;
        private string _text = "";

        public FiltrirajCommand(ICollectionView data)
        {
            _data = data;
        }

        public override void Execute(object parameter)
        {
            object[] parameters = (object[])parameter;
            _statusSelectedIndex = (int)parameters[0];
            _dodatoSelectedIndex = (int)parameters[1];
            _prioritetSelectedIndex = (int)parameters[2];
            _text = (string)parameters[3];
            _searchCatID = (int)parameters[4];
            _data.Filter = NapraviFilter;
        }

        private bool NapraviFilter(object obj)
        {
            Reklamacija curr = (Reklamacija)obj;

            if (_text != "")
            { 
                switch (_searchCatID)
                {
                    case 0:
                        if (!curr.ID.ToString().Contains(_text, StringComparison.OrdinalIgnoreCase)) return false;
                        break;
                    case 1:
                        if (!curr.Kupac.Contains(_text, StringComparison.OrdinalIgnoreCase)) return false;  
                        break;
                    case 2:
                        if (!curr.Dobavljac.Contains(_text, StringComparison.OrdinalIgnoreCase)) return false;
                        break;
                    case 3:
                        if (!curr.Proizvodjac.Contains(_text, StringComparison.OrdinalIgnoreCase)) return false;
                        break;
                    case 4:
                        if (!curr.NazivModela.Contains(_text, StringComparison.OrdinalIgnoreCase)) return false;
                        break;
                    case 5:
                        if (!curr.Serijski.Contains(_text, StringComparison.OrdinalIgnoreCase)) return false;
                        break;
                    case 6:
                        string s = curr.ID.ToString() + "ć" + curr.Kupac + "ć" + curr.Dobavljac + "ć" + curr.Proizvodjac + "ć" + 
                            curr.NazivModela + "ć" + curr.Serijski;
                        if (!s.Contains(_text, StringComparison.OrdinalIgnoreCase)) return false;
                        break;

                    default:
                        break;
                }
            }



            switch (_statusSelectedIndex)
            {
                case 0:
                    break;

                case 1:
                    if (curr.Status == "zavrseno");
                    else return false;
                    break;

                case 2:
                    if (curr.Status == "u toku");
                    else return false;
                    break;

                case 3:
                    if (curr.Status == "sa greskom");
                    else return false;
                    break;

                default:
                    break;
            }

            TimeSpan prosloVreme = DateTime.Now.Subtract(curr.Dodato);

            switch (_dodatoSelectedIndex)
            {
                case 0:
                    break;

                case 1:
                    if (!(prosloVreme.Days < 1)) return false;
                    break;

                case 2:
                    if (!(prosloVreme.Days < 7)) return false;
                    break;

                case 3:
                    if (!(prosloVreme.Days < 15)) return false;
                    break;

                case 4:
                    if (!(prosloVreme.Days < 30)) return false;
                    break;

                default:
                    break;
            }

            switch (_prioritetSelectedIndex)
            {
                case 0:
                    break;

                default:
                    if (curr.Prioritet == _prioritetSelectedIndex - 1); 
                    else return false;
                    break;
            }

            return true;
        }
    }
}
