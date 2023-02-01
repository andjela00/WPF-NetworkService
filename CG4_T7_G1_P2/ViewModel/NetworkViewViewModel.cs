using CG4_T7_G1_P2.Common;
using CG4_T7_G1_P2.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace CG4_T7_G1_P2.ViewModel
{
    public class NetworkViewViewModel : BindableBase
    {
        public static int monitor = 0;
        private ListView lv;
        public BindingList<MerniInstrument> Items { get; set; }
        public MyICommand<ListView> MLBUCommand { get; set; }
        public MyICommand<MerniInstrument> SCCommand { get; set; }
        public MyICommand<Canvas> DCommand { get; set; }
        public MyICommand<Canvas> FreeCommand { get; set; }
        public MyICommand<Canvas> LCommand { get; set; }
        public MyICommand<ListView> LLWCommand { get; set; }

        public MyICommand<Grid> AddSource { get; set; }

        Dictionary<int, MerniInstrument> temp = new Dictionary<int, MerniInstrument>();
        public static MerniInstrument draggedItem = null;
        private bool dragging = false;
        private static bool exists = false;
        private int selectedIndex = 0;
        int count = 0;

        public int SelectedIndex
        {
            get { return selectedIndex; }
            set
            {
                selectedIndex = value;
                OnPropertyChanged("SelectedIndex");
            }
        }

        public NetworkViewViewModel()
        {
            foreach (MerniInstrument ob in DataBase.MerniInstrumenti)
            {
                temp.Add(ob.Id, ob);
                count++;
            }
            Items = new BindingList<MerniInstrument>();
            foreach (var item in DataBase.MerniInstrumenti)
            {
                exists = false;
                foreach (var ex in DataBase.CanvasObjects.Values)
                {
                    if (ex.Id == item.Id)
                    {
                        exists = true;
                        break;
                    }
                }

                if (exists == false)
                    Items.Add(new MerniInstrument(item));
            }
            MLBUCommand = new MyICommand<ListView>(OnMLBU);
            SCCommand = new MyICommand<MerniInstrument>(SelectionChange);
            DCommand = new MyICommand<Canvas>(OnDrop);
            FreeCommand = new MyICommand<Canvas>(OnFree);
            LCommand = new MyICommand<Canvas>(OnLoad);
            LLWCommand = new MyICommand<ListView>(OnLLW);
            AddSource = new MyICommand<Grid>(OnAdd, CanAdd);
        }

        public void OnLLW(ListView listview)
        {
            lv = listview;
        }

        public void OnLoad(Canvas c)
        {
            if (DataBase.CanvasObjects.ContainsKey(c.Name))
            {
                BitmapImage logo = new BitmapImage();
                logo.BeginInit();
                logo.UriSource = new Uri(DataBase.CanvasObjects[c.Name].Type.Slika);
                logo.EndInit();
                c.Background = new ImageBrush(logo);
                c.Resources.Add("taken", true);
                monitor++;
                CheckValue(c);
            }
        }

        public void OnFree(Canvas c)
        {
            try
            {
                if (c.Resources["taken"] != null)
                {
                    c.Background = new SolidColorBrush(Color.FromRgb(255, 165, 0));
                    ((Border)c.Children[0]).BorderBrush = Brushes.Transparent;

                    foreach (var item in DataBase.MerniInstrumenti)
                    {
                        if (!Items.Contains(item) && DataBase.CanvasObjects[c.Name].Id == item.Id)
                        {
                            Items.Add(new MerniInstrument(item));
                        }
                    }
                    c.Resources.Remove("taken");
                    DataBase.CanvasObjects.Remove(c.Name);
                }
                monitor--;
            }
            catch (Exception) { }
            AddSource.RaiseCanExecuteChanged();
        }

        public void OnDrop(Canvas c)
        {
            if (draggedItem != null)
            {
                if (c.Resources["taken"] == null)
                {
                    BitmapImage logo = new BitmapImage();
                    logo.BeginInit();
                    logo.UriSource = new Uri(draggedItem.Type.Slika);
                    logo.EndInit();
                    c.Background = new ImageBrush(logo);
                    DataBase.CanvasObjects[c.Name] = draggedItem;
                    c.Resources.Add("taken", true);
                    Items.Remove(Items.Single(x => x.Id == draggedItem.Id));
                    SelectedIndex = 1;
                    monitor++;
                    CheckValue(c);
                }
                dragging = false;
            }
            AddSource.RaiseCanExecuteChanged();
        }

        public void OnMLBU(ListView lw)
        {
            draggedItem = null;
            lw.SelectedItem = null;
            dragging = false;
        }

        public void SelectionChange(MerniInstrument o)
        {
            if (!dragging)
            {
                dragging = true;
                draggedItem = new MerniInstrument(o);
                DragDrop.DoDragDrop(lv, draggedItem, DragDropEffects.Move);
            }
        }

        private void CheckValue(Canvas c)
        {

            foreach (MerniInstrument ob in DataBase.MerniInstrumenti)
            {
                temp[ob.Id] = ob;
            }
            Task.Delay(1000).ContinueWith(_ =>
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    if (DataBase.CanvasObjects.Count != 0)
                    {
                        if (DataBase.CanvasObjects.ContainsKey(c.Name))
                        {
                            if (temp[DataBase.CanvasObjects[c.Name].Id].Value < 250 || temp[DataBase.CanvasObjects[c.Name].Id].Value > 350)
                            {
                                ((Border)c.Children[0]).BorderBrush = Brushes.Red;
                            }
                            else
                            {
                                ((Border)c.Children[0]).BorderBrush = Brushes.Transparent;
                            }
                        }
                        else
                        {
                            ((Border)c.Children[0]).BorderBrush = Brushes.Transparent;
                        }
                    }
                });
                CheckValue(c);
            });

        }

        public void OnAdd(Grid allCanvas)
        {
            int i = 0;
            int len = Items.Count();
            foreach (Canvas c in allCanvas.Children)
            {
                if (c.Resources["taken"] == null)
                {
                    MerniInstrument v = new MerniInstrument(Items[i]);
                    BitmapImage logo = new BitmapImage();
                    logo.BeginInit();
                    logo.UriSource = new Uri(v.Type.Slika);
                    logo.EndInit();
                    c.Background = new ImageBrush(logo);
                    DataBase.CanvasObjects[c.Name] = v;
                    c.Resources.Add("taken", true);
                    SelectedIndex = 0;
                    monitor++;
                    CheckValue(c);
                    i++;
                    if (i == len)
                    {
                        break;
                    }
                }
            }
            Items = null;
            Items = new BindingList<MerniInstrument>();
            AddSource.RaiseCanExecuteChanged();
        }

        public bool CanAdd(Grid grid)
        {
            if (Items.Count > 0 && Items.Count < 15)
                return true;
            return false;
        }
    }
}
