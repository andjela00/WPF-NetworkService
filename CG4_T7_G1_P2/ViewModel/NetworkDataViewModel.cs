using CG4_T7_G1_P2.Common;
using CG4_T7_G1_P2.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CG4_T7_G1_P2.ViewModel
{
    public class NetworkDataViewModel : BindableBase
    {
        private DataBase dataBase = new DataBase();
        public List<MerniInstrument> SerachSources = new List<MerniInstrument>();
        public ObservableCollection<MerniInstrument> CopySources { get; set; } = new ObservableCollection<MerniInstrument>();
        public Dictionary<int, MerniInstrument> Reserve { get; set; } = new Dictionary<int, MerniInstrument>();
        public List<string> ComboBoxData { get; set; }      //kombo box za pretragu 
        public List<string> ComboBoxDataSource { get; set; }   // za dodavanje
        public MyICommand AddCommand { get; set; }      //komande za dugmice
        public MyICommand DeleteCommand { get; set; }
        public MyICommand SearchCommand { get; set; }
        public MyICommand LowerCommand { get; set; }
        public MyICommand GreaterCommand { get; set; }
        private string idFilter;
        public ObservableCollection<MerniInstrument> Izvori { get; set; } = new ObservableCollection<MerniInstrument>();

        public MyICommand OutOfRangeFilterCommand { get; set; }
        public MyICommand ExpectedFilterCommand { get; set; }
        public MyICommand ShowAllCommand { get; set; }
        //id reaktora kako dodamo novi id se poveca
        private string idSearch;
        private string typeText;
        private string id;
        private string name;
        private string searchValueText;
        private string filterText;
        private int higherOrLower = -1;
        //private string outOrExpected = "";

        private int index = -1;
        private string path;
        private int clickSearch = 0;
        private string nameButtonSearch = "Search";
        string pathSaveSearch = Environment.CurrentDirectory + @"\SaveSearch.txt";
        private string valueLess;
        private string valueGreater;
        private bool idCheck = false;
        private bool show = false;
        private float brojTermoSprege;
        private float brojrtd;
        private string margina = "10";
        private int rtd, termosprega;

        public NetworkDataViewModel()
        {
            if (Izvori.Count() > 0)
            {
                Izvori.Clear();
            }
            foreach (var i in DataBase.MerniInstrumenti)
            {
                Izvori.Add(i);
            }
            ComboBoxData = new List<string>();
            ComboBoxDataSource = new List<string> { "RTD", "TermoSprega" };
            foreach (MerniInstrument r in Izvori)
            {
                if (!Reserve.ContainsKey(r.Id))
                {
                    Reserve.Add(r.Id, r);
                }
            }
            int ukupanBroj = Izvori.Count;
            if (ukupanBroj != 0)
            {
                foreach (var v in Izvori)
                {

                    if (v.Type.Name == "TermoSprega")
                    {
                        termosprega++;
                    }
                    else
                    {
                        rtd++;
                    }
                }
                
                Margina = $"0,62,{20 + brojTermoSprege},0";
                
            }


            ValueLess = "True";

            HigherOrLower = 0;
            //OutOrExpected = "";

            AddCommand = new MyICommand(OnAdd, CanAdd);
            DeleteCommand = new MyICommand(OnDelete, CanDelete);
            SearchCommand = new MyICommand(OnSearch, CanSearch);
            LowerCommand = new MyICommand(OnLower);
            GreaterCommand = new MyICommand(OnGreater);

            OutOfRangeFilterCommand = new MyICommand(OnOutOfRange);
            ExpectedFilterCommand = new MyICommand(OnExpected);

            ShowAllCommand = new MyICommand(OnShow, CanShow);
        }

        public DataBase DataBase
        { get => dataBase; set => dataBase = value; }

        public bool Show
        {
            get { return show; }
            set
            {
                show = value;
                ShowAllCommand.RaiseCanExecuteChanged();
            }
        }

        private bool CanShow()
        {
            if (Show)
            {
                return true;
            }
            return false;
        }

        private void OnShow()
        {
            Izvori.Clear();
            foreach (var item in Reserve.Values)
            {
                Izvori.Add(item);
            }
            Show = false;
        }


        private void OnOutOfRange()
        {
            Show = true;
            SerachSources.Clear();
            foreach (var item in Reserve.Values)
            {
                if (item.Value < 250 || item.Value > 350)
                {
                    SerachSources.Add(item);
                }
            }
            Izvori.Clear();
            foreach (MerniInstrument v in SerachSources)
            {
                Izvori.Add(v);
            }
        }

        private void OnExpected()
        {
            Show = true;
            SerachSources.Clear();
            foreach (var item in Reserve.Values)
            {
                if (item.Value >= 250 && item.Value <= 350)
                {
                    SerachSources.Add(item);
                }
            }
            Izvori.Clear();
            foreach (MerniInstrument v in SerachSources)
            {
                Izvori.Add(v);
            }
        }

        public string FilterText
        {
            get { return filterText; }
            set
            {
                filterText = value;
                ShowAllCommand.RaiseCanExecuteChanged();
            }
        }

        public int HigherOrLower
        {
            get { return higherOrLower; }
            set
            {
                higherOrLower = value;
            }
        }

        public string IdFilter
        {
            get { return idFilter; }
            set
            {
                idFilter = value;
                OnPropertyChanged("IdFilter");
                SearchCommand.RaiseCanExecuteChanged();
            }
        }
        public string IdSearch
        {
            get { return idSearch; }
            set
            {
                idSearch = value;
                OnPropertyChanged("IdSearch");   
                SearchCommand.RaiseCanExecuteChanged();
            }
        }

        public string Id
        {
            get { return id; }
            set
            {
                id = value;
                OnPropertyChanged("Id");
                AddCommand.RaiseCanExecuteChanged();
            }
        }
        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                OnPropertyChanged("Name");
                AddCommand.RaiseCanExecuteChanged();
            }
        }
        public string TypeText
        {
            get { return typeText; }
            set
            {
                typeText = value;
                OnPropertyChanged("TypeText");
                Path = Environment.CurrentDirectory;
                Path = Path.Remove(Path.Length - 10, 10);
                Path += @"\Pictures\" + value + ".jpg";
                AddCommand.RaiseCanExecuteChanged(); 
            }
        }
        public string SearchValueText
        {
            get { return searchValueText; }
            set
            {
                searchValueText = value;
                OnPropertyChanged("SearchValueText");
                SearchCommand.RaiseCanExecuteChanged();
            }
        }
        public int Index
        {
            get { return index; }
            set
            {
                index = value;
                DeleteCommand.RaiseCanExecuteChanged();
            }
        }
        public string Path
        {
            get { return path; }
            set
            {
                path = value;
                OnPropertyChanged("Path");
            }
        }

        public float Brojrtd
        {
            get
            {
                return brojrtd;
            }

            set
            {
                brojrtd = value;
                OnPropertyChanged("Brojrtd");
            }
        }

        public string Margina
        {
            get
            {
                return margina;
            }

            set
            {
                margina = value;
                OnPropertyChanged("Margina");
            }
        }

        public float BrojTermoSprege
        {
            get
            {
                return brojTermoSprege;
            }
            set
            {
                brojTermoSprege = value;
                OnPropertyChanged("BrojTermoSprege");
            }
        }

        public int ClickSearch
        {
            get { return clickSearch; }
            set
            {
                clickSearch = value;
                if (value == 0)
                    NameButtonSearch = "Search";
                else
                    NameButtonSearch = "UndoSearch";
            }
        }
        public string NameButtonSearch
        {
            get { return nameButtonSearch; }
            set
            {
                nameButtonSearch = value;
                OnPropertyChanged("NameButtonSearch");
            }
        }

        public string ValueLess
        {
            get { return valueLess; }
            set
            {
                valueLess = value;
                OnPropertyChanged("ValueLess");
            }
        }

        public string ValueGreater
        {
            get { return valueGreater; }
            set
            {
                valueGreater = value;
                OnPropertyChanged("ValueGreater");
            }
        }

        private bool CanDelete()
        {
            return index >= 0;
        }
        private void OnDelete()
        {
            bool valid = true;
            foreach (MerniInstrument v in DataBase.CanvasObjects.Values)
            {
                if (v.Name == Izvori[index].Name)
                {
                    valid = false;
                    MessageBox.Show("Source monitoring in porgres..", "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
                    break;
                }
            }

            if (valid)
            {
                int i = index;
                Izvori.RemoveAt(i);
                DataBase.MerniInstrumenti.RemoveAt(i);

            }

        }
        public int idInt;
        private bool CanAdd()
        {
            if (TypeText != null && Name != null && Name != "")//provera da li je odabran tip reaktora
            {
                return true;
            }
            return false;

        }

        private void OnAdd()
        {
            if (Int32.TryParse(Id, out idInt))
            {
                bool postoji = false;
                if (DataBase.MerniInstrumenti.Count != 0)
                {
                    foreach (MerniInstrument v in DataBase.MerniInstrumenti)
                    {
                        if (idInt == v.Id)
                        {
                            MessageBox.Show("Source with that id already exists.", "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
                            postoji = true;
                        }
                    }
                }
                if (!postoji)
                {
                    MerniInstrument i = new MerniInstrument();
                    i.Id = idInt;
                    i.Name = Name;
                    i.Type.Name = TypeText;
                    string pathImg = Environment.CurrentDirectory;
                    pathImg = pathImg.Remove(pathImg.Length - 10, 10);
                    pathImg += @"\Pictures\" + TypeText + ".jpg";
                    i.Type.Slika = pathImg;
                    Izvori.Add(i);
                    DataBase.MerniInstrumenti.Add(i);
                    foreach (MerniInstrument r in DataBase.MerniInstrumenti)
                    {
                        if (!Reserve.ContainsKey(r.Id))
                        {
                            Reserve.Add(r.Id, r);
                        }
                    }
                    int ukupanBroj = Izvori.Count;
                    termosprega = 0;
                    rtd = 0;

                    if (ukupanBroj != 0)
                    {
                        foreach (var v in Izvori)
                        {

                            if (v.Type.Name == "TermoSprega")
                            {
                                termosprega++;
                            }
                            else
                            {
                                rtd++;
                            }
                        }
                        BrojTermoSprege = 300 * (((100 / (float)ukupanBroj) * (float)termosprega) / 100);
                        Margina = $"0,62,{20 + brojTermoSprege},0";
                        Brojrtd = 300 * (((100 / (float)ukupanBroj) * (float)rtd) / 100);
                    }
                }
            }
            else
            {
                MessageBox.Show("Id must be a INT32 number", "Warning!", MessageBoxButton.OK, MessageBoxImage.Warning);
            }


        }

        private void OnLower()
        {
            HigherOrLower = 0;
        }
        public void OnGreater()
        {
            HigherOrLower = 1;
        }

        private bool CanSearch()
        {
            if (FilterText != null && FilterText != "" && IdFilter != null && IdFilter != "")
                return true;
            return false;
        }

        private void OnSearch()
        {
            if (Int32.TryParse(IdFilter, out fint))
            {
                if (ClickSearch == 0)    //1 kad moze undo inace 0
                {
                    foreach (MerniInstrument r in Izvori)
                    {
                        CopySources.Add(r);
                    }

                    if (HigherOrLower == 0) // manje
                    {
                        foreach (var item in Izvori)
                        {
                            if (item.Type.Name.Equals(FilterText) && item.Id < fint)
                            {
                                SerachSources.Add(item);
                            }
                        }
                    }
                    else if (HigherOrLower == 1) //vece
                    {

                        foreach (var item in Izvori)
                        {
                            if (item.Type.Name.Equals(FilterText) && item.Id > fint)
                            {
                                SerachSources.Add(item);
                            }
                        }

                    }
                    Izvori.Clear();
                    foreach (MerniInstrument v in SerachSources)
                    {
                        Izvori.Add(v);
                    }
                    SerachSources.Clear();
                    ClickSearch = 1;

                }
                else
                {
                    Izvori.Clear();
                    foreach (MerniInstrument v in Reserve.Values)
                    {
                        Izvori.Add(v);
                    }
                    CopySources.Clear();
                    ClickSearch = 0;
                }
            }
            else
            {
                MessageBox.Show("Id mora da bude int ", "Warning!", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        public int fint;
        private void onFilter()
        {
            if (Int32.TryParse(IdFilter, out fint))
            {
                
            }
            else
            {
                MessageBox.Show("Id mora da bude int ", "Warning!", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }



    }
}
