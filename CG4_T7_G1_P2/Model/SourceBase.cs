﻿using CG4_T7_G1_P2.Common;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CG4_T7_G1_P2.Model
{
    public class DataBase
    {
        public static ObservableCollection<MerniInstrument> MerniInstrumenti { get; set; } = new ObservableCollection<MerniInstrument>();
        public static Dictionary<string, MerniInstrument> CanvasObjects { get; set; } = new Dictionary<string, MerniInstrument>();
    }

    public class MerniInstrument : BindableBase
    {
        private int id;
        private string name;
        private Type type = new Type();
        private double value;
        public int Id
        {
            get { return id; }
            set
            {
                id = value;
            }
        }
        public string Name
        {
            get { return name; }
            set
            {
                name = value;
            }
        }

        public double Value
        {
            get { return value; }
            set
            {
                this.value = value;
                OnPropertyChanged("Value");
                int a = ViewModel.DataChartViewModel.SourceChoice;
                if (a == this.id)
                {
                    if (value <= 350 && value >= 250)
                    {
                        ViewModel.DataChartViewModel.ElementHeights.FirstBindingPoint = ViewModel.DataChartViewModel.CalculateElementHeight(value, this.id);
                        ViewModel.DataChartViewModel.ElementHeights.FirstBindingPoint1 = (ViewModel.DataChartViewModel.CalculateElementHeight(value, this.id));
                        ViewModel.DataChartViewModel.ElementHeights.FirstBindingPoint2 = "Green";
                    }
                    else if (value > 350 || value < 250)
                    {
                        ViewModel.DataChartViewModel.ElementHeights.FirstBindingPoint = ViewModel.DataChartViewModel.CalculateElementHeight(value, this.id);
                        ViewModel.DataChartViewModel.ElementHeights.FirstBindingPoint1 = (ViewModel.DataChartViewModel.CalculateElementHeight(value, this.id));
                        ViewModel.DataChartViewModel.ElementHeights.FirstBindingPoint2 = "Red";
                    }
                }
            }
        }



        public Type Type
        {
            get { return type; }
            set
            {
                type.Name = value.Name;
                type.Slika = value.Slika;
            }
        }



        public MerniInstrument()
        {
        }
        public MerniInstrument(MerniInstrument s)
        {
            Id = s.Id;
            Name = s.Name;
            Type = s.type;
            Value = s.Value;
        }
    }
}
