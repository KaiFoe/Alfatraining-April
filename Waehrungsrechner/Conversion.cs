using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using UIKit;

namespace Waehrungsrechner
{
    public class Conversion
    {
        public string to { get; set; }
        public string date { get; set; }
        public double rate { get; set; }

        public Conversion() { }
    }
}