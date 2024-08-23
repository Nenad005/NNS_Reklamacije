using NNS_Reklamacije.Models;
using System.Collections.Specialized;
using System.Xml;

namespace NNS_Reklamacije.Views
{
    /// <summary>
    /// Interaction logic for ReklamacijeView.xaml
    /// </summary>
    public partial class ReklamacijeView : UserControl
    {
        private List<string> kupci = new();
        private List<string> ids = new();
        private List<string> dobavljaci = new();
        private List<string> proizvodjaci = new();
        private List<string> modeli = new();
        private List<string> serijski = new();

        public ReklamacijeView()
        {
            InitializeComponent();

            listView.Loaded += ListView_Loaded;

            comboBox.SelectionChanged += ComboBox_SelectionChanged;
            statusComboBox.SelectionChanged += ComboBox_SelectionChanged;
            dodatoComboBox.SelectionChanged += ComboBox_SelectionChanged;
            prioritetComboBox.SelectionChanged += ComboBox_SelectionChanged;

            autoSugesstion.autoTextBox.KeyDown += EnterKeyDown;
            autoSugesstion.autoTextBox.TextChanged += AutoTextBox_TextChanged;
            autoSugesstion.autoTextBox.GotFocus += AutoTextBox_GotFocus;
            //((INotifyCollectionChanged)listView.Items).CollectionChanged += CollectionChanged;

            listView.Items.SortDescriptions.Add(new SortDescription("ID", ListSortDirection.Descending));

            comboBox.Items.Add("ID");
            comboBox.Items.Add("Kupac");
            comboBox.Items.Add("Dobavljač");
            comboBox.Items.Add("Proizvođač");
            comboBox.Items.Add("Model");
            comboBox.Items.Add("Serijski Br.");
            comboBox.Items.Add("Sve");
            comboBox.SelectedIndex = 6;

            statusComboBox.Items.Add("svi");
            statusComboBox.Items.Add("završeni");
            statusComboBox.Items.Add("u toku");
            
            dodatoComboBox.Items.Add("svi");
            dodatoComboBox.Items.Add("danas");
            dodatoComboBox.Items.Add("zadnjih 7 dana");
            dodatoComboBox.Items.Add("zadnjih 15 dana");
            dodatoComboBox.Items.Add("zadnjih 30 dana");

            prioritetComboBox.Items.Add("svi");
            prioritetComboBox.Items.Add("nizak");
            prioritetComboBox.Items.Add("normalan");
            prioritetComboBox.Items.Add("visok");

            listView.MouseDoubleClick += listViewDoubleClick;
            listView.SelectionChanged += ListView_SelectionChanged;

            printButton.Click += PrintButton_Click;
        }

        private void PrintButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Reklamacija curr = listView.Items[listView.SelectedIndex] as Reklamacija;

                StampanjeWindow prozor = new StampanjeWindow(curr);
                prozor.ShowDialog();
            }
            catch (Exception)
            {
                MessageBox.Show("greska");
            }

            
        }

        private void AutoTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (autoSugesstion.Text == "") Filter();
        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (listView.SelectedItems.Count <= 1) return;
            foreach (var item in e.AddedItems)
            {
                listView.SelectedItems.Remove(item);
            }
        }

        private void listViewDoubleClick(object sender, MouseButtonEventArgs e)
        {
            DependencyObject src = (DependencyObject)(e.OriginalSource);
            while (!(src is Control))
            { 
                src = VisualTreeHelper.GetParent(src);
                //if (src.GetType().Name == "ListViewItem") MessageBox.Show($"izarbani index {(listView.Items[listView.SelectedIndex] as Reklamacija).ID}");
                if (src.GetType().Name == "ListViewItem") (DataContext as ReklamacijeViewModel).UnesiReklamaciju.Execute(listView);
            }
        }

        private void AutoTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            LoadLists();
        }

        private void EnterKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (autoSugesstion.Text == "/podesavanja")
                {
                    (DataContext as ReklamacijeViewModel).Podesavanja.Execute(null);
                }
                Filter();
                autoSugesstion.autoTextBox.SelectAll();
            }
            //if (e.Key == Key.Enter)
            //{
            //    string toSearch = autoSugesstion.autoTextBox.Text;

            //    if (toSearch != prevToSearch)
            //    { 
            //        lastSearchIndex = GetLastSearchIndex(toSearch);
            //        prevSearchindex = -1;
            //        prevToSearch = toSearch;
            //    }

            //    switch (comboBox.SelectedIndex)
            //    {
            //        case 0:
            //            for (int i = 0; i < listView.Items.Count; i++)
            //            {
            //                Reklamacija curr = (Reklamacija)listView.Items[i];

            //                if ( (curr.ID.ToString() == toSearch ) && i > prevSearchindex)
            //                {
            //                    listView.ScrollIntoView(listView.Items[i]);
            //                    listView.SelectedIndex = i;
            //                    prevSearchindex = i;

            //                    if (i == lastSearchIndex) prevSearchindex = -1;
            //                    break;
            //                }
            //            }
            //            break;

            //        case 1:
            //            for (int i = 0; i < listView.Items.Count; i++)
            //            {
            //                Reklamacija curr = (Reklamacija)listView.Items[i];

            //                if ((curr.Kupac == toSearch) && i > prevSearchindex)
            //                {
            //                    listView.ScrollIntoView(listView.Items[i]);
            //                    listView.SelectedIndex = i;
            //                    prevSearchindex = i;

            //                    if (i == lastSearchIndex) prevSearchindex = -1;
            //                    break;
            //                }
            //            }
            //            break;

            //        case 2:
            //            for (int i = 0; i < listView.Items.Count; i++)
            //            {
            //                Reklamacija curr = (Reklamacija)listView.Items[i];

            //                if ((curr.Dobavljac == toSearch) && i > prevSearchindex)
            //                {
            //                    listView.ScrollIntoView(listView.Items[i]);
            //                    listView.SelectedIndex = i;
            //                    prevSearchindex = i;

            //                    if (i == lastSearchIndex) prevSearchindex = -1;
            //                    break;
            //                }
            //            }
            //            break;

            //        case 3:
            //            for (int i = 0; i < listView.Items.Count; i++)
            //            {
            //                Reklamacija curr = (Reklamacija)listView.Items[i];

            //                if ((curr.Proizvodjac == toSearch) && i > prevSearchindex)
            //                {
            //                    listView.ScrollIntoView(listView.Items[i]);
            //                    listView.SelectedIndex = i;
            //                    prevSearchindex = i;

            //                    if (i == lastSearchIndex) prevSearchindex = -1;
            //                    break;
            //                }
            //            }
            //            break;

            //        case 4:
            //            for (int i = 0; i < listView.Items.Count; i++)
            //            {
            //                Reklamacija curr = (Reklamacija)listView.Items[i];

            //                if ((curr.NazivModela == toSearch) && i > prevSearchindex)
            //                {
            //                    listView.ScrollIntoView(listView.Items[i]);
            //                    listView.SelectedIndex = i;
            //                    prevSearchindex = i;

            //                    if (i == lastSearchIndex) prevSearchindex = -1;
            //                    break;
            //                }
            //            }
            //            break;

            //        case 5:
            //            for (int i = 0; i < listView.Items.Count; i++)
            //            {
            //                Reklamacija curr = (Reklamacija)listView.Items[i];

            //                if ((curr.Serijski == toSearch) && i > prevSearchindex)
            //                {
            //                    listView.ScrollIntoView(listView.Items[i]);
            //                    listView.SelectedIndex = i;
            //                    prevSearchindex = i;

            //                    if (i == lastSearchIndex) prevSearchindex = -1;
            //                    break;
            //                }
            //            }
            //            break;

            //        default:
            //            break;
            //    }
            //}
        }

        //private int GetLastSearchIndex(string toSearch)
        //{
        //    switch (comboBox.SelectedIndex)
        //    {
        //        case 0:
        //            for (int i = listView.Items.Count - 1; i >= 0 ; i--)
        //            {
        //                Reklamacija curr = (Reklamacija)listView.Items[i];

        //                if (curr.ID.ToString() == toSearch) return i;
        //            }
        //            break;

        //        case 1:
        //            for (int i = listView.Items.Count - 1; i >= 0; i--)
        //            {
        //                Reklamacija curr = (Reklamacija)listView.Items[i];

        //                if (curr.Kupac == toSearch) return i;
        //            }
        //            break;

        //        case 2:
        //            for (int i = listView.Items.Count - 1; i >= 0; i--)
        //            {
        //                Reklamacija curr = (Reklamacija)listView.Items[i];

        //                if (curr.Dobavljac == toSearch) return i;
        //            }
        //            break;

        //        case 3:
        //            for (int i = listView.Items.Count - 1; i >= 0; i--)
        //            {
        //                Reklamacija curr = (Reklamacija)listView.Items[i];

        //                if (curr.Proizvodjac == toSearch) return i;
        //            }
        //            break;

        //        case 4:
        //            for (int i = listView.Items.Count - 1; i >= 0; i--)
        //            {
        //                Reklamacija curr = (Reklamacija)listView.Items[i];

        //                if (curr.NazivModela == toSearch) return i;
        //            }
        //            break;

        //        case 5:
        //            for (int i = listView.Items.Count - 1; i >= 0; i--)
        //            {
        //                Reklamacija curr = (Reklamacija)listView.Items[i];

        //                if (curr.Serijski == toSearch) return i;
        //            }
        //            break;

        //        default:
        //            break;
        //    }
        //    return -1;
        //}

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (statusComboBox.SelectedIndex != 2) prioritetComboBox.SelectedIndex = 0;
            Filter();
        }

        private void Filter()
        {
            if (DataContext == null) return;
            (DataContext as ReklamacijeViewModel).Filtritraj.Execute(new object[]
            { statusComboBox.SelectedIndex, dodatoComboBox.SelectedIndex,
                prioritetComboBox.SelectedIndex, autoSugesstion.Text, comboBox.SelectedIndex});
        }

        private async void ListView_Loaded(object sender, RoutedEventArgs e)
        {
            (DataContext as ReklamacijeViewModel).GetData.Execute(null);
            //MessageBox.Show(Directory.GetCurrentDirectory());
            //ScrollDown();
            //loaded = true;
        }

        private void LoadLists()
        {
            try
            {
                switch (comboBox.SelectedIndex)
                {
                    case 0:
                        List<string> _ids = new();
                        foreach (var item in listView.Items)
                        {

                            Reklamacija curr = (Reklamacija)item;

                            if (!_ids.Contains(curr.ID.ToString()))
                            {
                                _ids.Add(curr.ID.ToString());
                            }
                        }
                        ids = _ids;
                        break;

                    case 1:
                        List<string> _kupci = new();
                        foreach (var item in listView.Items)
                        {
                            Reklamacija curr = (Reklamacija)item;

                            if (!_kupci.Contains(curr.Kupac))
                            {
                                _kupci.Add(curr.Kupac);
                            }
                        }
                        kupci = _kupci;
                        break;

                    case 2:
                        List<string> _dobavljaci = new();
                        foreach (var item in listView.Items)
                        {
                            Reklamacija curr = (Reklamacija)item;

                            if (!_dobavljaci.Contains(curr.Dobavljac))
                            {
                                _dobavljaci.Add(curr.Dobavljac);
                            }
                        }
                        dobavljaci = _dobavljaci;
                        break;
                    
                    case 3:
                        List<string> _proizvodjaci = new();
                        foreach (var item in listView.Items)
                        {
                            Reklamacija curr = (Reklamacija)item;

                            if (!_proizvodjaci.Contains(curr.Proizvodjac))
                            {
                                _proizvodjaci.Add(curr.Proizvodjac);
                            }
                        }
                        proizvodjaci = _proizvodjaci;
                        break;

                    case 4:
                        List<string> _modeli = new();
                        foreach (var item in listView.Items)
                        {
                            Reklamacija curr = (Reklamacija)item;

                            if (!_modeli.Contains(curr.NazivModela))
                            {
                                _modeli.Add(curr.NazivModela);
                            }
                        }
                        modeli = _modeli;
                        break;

                    case 5:
                        List<string> _serijski = new();
                        foreach (var item in listView.Items)
                        {
                            Reklamacija curr = (Reklamacija)item;

                            if (!_serijski.Contains(curr.Serijski))
                            {
                                _serijski.Add(curr.Serijski);
                            }
                        }
                        serijski = _serijski;
                        break;

                    case -1:
                        return;

                    default:
                        break;
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                Apply_Lists();
            }
        }

        private void Apply_Lists()
        {
            switch (comboBox.SelectedIndex)
            {
                case 0:
                    autoSugesstion.AutoSuggestionList = ids;
                    break;

                case 1:
                    autoSugesstion.AutoSuggestionList = kupci;
                    break;

                case 2:
                    autoSugesstion.AutoSuggestionList = dobavljaci;
                    break;
                
                case 3:
                    autoSugesstion.AutoSuggestionList = proizvodjaci;
                    break;
                
                case 4:
                    autoSugesstion.AutoSuggestionList = modeli;
                    break;

                case 5:
                    autoSugesstion.AutoSuggestionList = serijski;
                    break;

                case 6:
                    autoSugesstion.AutoSuggestionList = new List<string>();
                    break;

                case -1:
                    return;

                default:
                    break;
            }
        }

        //private void ScrollDown()
        //{
        //    if (listView != null)
        //    {
        //        if (listView.Items.Count != 0) listView.ScrollIntoView(listView.Items[listView.Items.Count - 1]);
        //    }
        //}
    }
}
