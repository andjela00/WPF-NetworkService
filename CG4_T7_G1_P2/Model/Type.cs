using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CG4_T7_G1_P2.Model
{
    public class Type
    {
        private string slika;
        private string name;

        public string Name { get => name; set => name = value; }
        public string Slika { get => slika; set => slika = value; }

        public Type() { }
        public Type(Type t)
        {
            Name = t.name;
            Slika = t.slika;
        }
    }
}
