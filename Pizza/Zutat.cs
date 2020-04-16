using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using UIKit;

namespace Pizza
{
    public class Zutat
    {
        public int id { get; set; }
        public string name { get; set; }

        public Zutat() { }
        public Zutat(string name)
        {
            this.name = name;
        }
    }
}