using CG4_T7_G1_P2.Common;
using CG4_T7_G1_P2.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CG4_T7_G1_P2.ViewModel
{
    public class DataChartViewModel : BindableBase
    {
        public Dictionary<int, MerniInstrument> ComboBoxDataSource { get; set; }   // za dodavanje
        public static GraphUpdater ElementHeights { get; set; } = new GraphUpdater();
        private static int sourceChoice;

        public DataChartViewModel()
        {
            ComboBoxDataSource = new Dictionary<int, MerniInstrument>();

            foreach (MerniInstrument r in DataBase.MerniInstrumenti)
            {
                if (!ComboBoxDataSource.ContainsKey(r.Id))
                {
                    ComboBoxDataSource.Add(r.Id, r);
                }
            }
        }

        public static int SourceChoice
        {
            get { return sourceChoice; }
            set
            {
                sourceChoice = value;
                List<int> vrednost = new List<int>();
                List<DateTime> dates = new List<DateTime>();
                string[] lines = File.ReadAllLines("Log.txt");
                List<String> l = lines.ToList();
                l.Reverse();
                foreach (string s in l)
                {
                    DateTime dt = DateTime.Parse($"{s.Split(':', '|')[5]}:{s.Split(':', '|')[6]}:{s.Split(':', '|')[7]}".Trim());
                    int id = int.Parse(s.Split(':', '|')[1]);
                    if (id == SourceChoice)
                    {
                        vrednost.Add(int.Parse(s.Split(':', '|')[3]));
                        dates.Add(dt);

                    }

                    if (dates.Count == 5)
                        break;
                }
                int duzina = vrednost.Count();
                int a = 3;

                if (duzina >= 1)
                {
                    if (vrednost[0] <= 350 && vrednost[0] >= 250)
                    {
                        ViewModel.DataChartViewModel.ElementHeights.FirstBindingPoint = vrednost[0];
                        ViewModel.DataChartViewModel.ElementHeights.FirstBindingPoint1 = (ViewModel.DataChartViewModel.CalculateElementHeight(vrednost[0], a));
                        ViewModel.DataChartViewModel.ElementHeights.FirstBindingPoint2 = "Green";
                    }
                    else if (vrednost[0] > 350 || vrednost[0] < 250)
                    {
                        ViewModel.DataChartViewModel.ElementHeights.FirstBindingPoint = vrednost[0];
                        ViewModel.DataChartViewModel.ElementHeights.FirstBindingPoint1 = (ViewModel.DataChartViewModel.CalculateElementHeight(vrednost[0], a));
                        ViewModel.DataChartViewModel.ElementHeights.FirstBindingPoint2 = "Red";
                    }
                }
                

                if (duzina >= 2)
                {
                    if (vrednost[1] <= 350 && vrednost[1] >= 250)
                    {
                        ViewModel.DataChartViewModel.ElementHeights.SecondBindingPoint = vrednost[1];
                        ViewModel.DataChartViewModel.ElementHeights.SecondBindingPoint1 = (ViewModel.DataChartViewModel.CalculateElementHeight(vrednost[1], a));
                        ViewModel.DataChartViewModel.ElementHeights.SecondBindingPoint2 = "Green";
                    }
                    else if (vrednost[1] > 350 || vrednost[1] < 250)
                    {
                        ViewModel.DataChartViewModel.ElementHeights.SecondBindingPoint =vrednost[1];
                        ViewModel.DataChartViewModel.ElementHeights.SecondBindingPoint1 = (ViewModel.DataChartViewModel.CalculateElementHeight(vrednost[1], a));
                        ViewModel.DataChartViewModel.ElementHeights.SecondBindingPoint2 = "Red";
                    }
                }
                

                if (duzina >= 3)
                {
                    if (vrednost[2] <= 350 && vrednost[2] >= 250)
                    {
                        ViewModel.DataChartViewModel.ElementHeights.ThirdBindingPoint = vrednost[2];
                        ViewModel.DataChartViewModel.ElementHeights.ThirdBindingPoint1 = (ViewModel.DataChartViewModel.CalculateElementHeight(vrednost[2], a));
                        ViewModel.DataChartViewModel.ElementHeights.ThirdBindingPoint2 = "Green";
                    }
                    else if (vrednost[2] > 350 || vrednost[2] < 250)
                    {
                        ViewModel.DataChartViewModel.ElementHeights.ThirdBindingPoint = vrednost[2];
                        ViewModel.DataChartViewModel.ElementHeights.ThirdBindingPoint1 = (ViewModel.DataChartViewModel.CalculateElementHeight(vrednost[2], a));
                        ViewModel.DataChartViewModel.ElementHeights.ThirdBindingPoint2 = "Red";
                    }
                }
                
                if (duzina >= 4)
                {
                    if (vrednost[3] <= 350 && vrednost[3] >= 250)
                    {
                        ViewModel.DataChartViewModel.ElementHeights.FourthBindingPoint =vrednost[3];
                        ViewModel.DataChartViewModel.ElementHeights.FourthBindingPoint1 = (ViewModel.DataChartViewModel.CalculateElementHeight(vrednost[3], a));
                        ViewModel.DataChartViewModel.ElementHeights.FourthBindingPoint2 = "Green";
                    }
                    else if (vrednost[3] > 350 || vrednost[3] < 250)
                    {
                        ViewModel.DataChartViewModel.ElementHeights.FourthBindingPoint =vrednost[2];
                        ViewModel.DataChartViewModel.ElementHeights.FourthBindingPoint1 = (ViewModel.DataChartViewModel.CalculateElementHeight(vrednost[2], a));
                        ViewModel.DataChartViewModel.ElementHeights.FourthBindingPoint2 = "Red";
                    }
                }
                
                if (duzina >= 5)
                {
                    if (vrednost[4] <= 350 && vrednost[4] >= 250)
                    {
                        ViewModel.DataChartViewModel.ElementHeights.FifthBindingPoint = vrednost[4];
                        ViewModel.DataChartViewModel.ElementHeights.FifthBindingPoint1 = (ViewModel.DataChartViewModel.CalculateElementHeight(vrednost[4], a));
                        ViewModel.DataChartViewModel.ElementHeights.FifthBindingPoint2 = "Green";
                    }
                    else if (vrednost[4] > 350 || vrednost[4] < 250)
                    {
                        ViewModel.DataChartViewModel.ElementHeights.FifthBindingPoint = vrednost[4];
                        ViewModel.DataChartViewModel.ElementHeights.FifthBindingPoint1 = (ViewModel.DataChartViewModel.CalculateElementHeight(vrednost[4], a));
                        ViewModel.DataChartViewModel.ElementHeights.FifthBindingPoint2 = "Red";
                    }
                }
                
            }
        }

        public static double CalculateElementHeight(double value, int a)
        {
            return value;
        }
    }
}





