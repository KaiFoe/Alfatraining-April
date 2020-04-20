using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CoreGraphics;
using Foundation;
using UIKit;

namespace Drawing
{
    class Polyline
    {
        //Farbe unserer Linie
        public CGColor Color { get; set; }

        //Dicke unserer Linie
        public float Thickness { get; set; }

        //Sammlung von Punkten
        //damit wir wissen wo die Linie gezeichnet werden soll
        public CGPath Path { get; set; }

        public Polyline()
        {
            Path = new CGPath();
        }
    }
}