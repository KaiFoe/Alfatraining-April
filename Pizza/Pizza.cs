using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using UIKit;

namespace Pizza
{
    public class Pizza
    {
        public int id { get; set; }
        public string name { get; set; }
        public List<Zutat> zutaten { get; set; }

        public Pizza() { }
        public Pizza(string name)
        {
            this.name = name;
            zutaten = new List<Zutat>();
        }
    }
}