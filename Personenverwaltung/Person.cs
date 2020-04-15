using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using UIKit;

namespace Personenverwaltung
{
    public class Person
    {
        int id { get; set; }
        public string vorname { get; set; }
        public string nachname { get; set; }
        public string plz { get; set; }
        public string strasse { get; set; }
        public string ort { get; set; }

        public Person() { }

        public Person(string vorname, string nachname)
        {
            this.vorname = vorname;
            this.nachname = nachname;
        }

        public override string ToString()
        {
            return vorname + " " + nachname;
        }
    }
}