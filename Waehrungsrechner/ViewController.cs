using Foundation;
using System;
using System.Collections.Generic;
using UIKit;

namespace Waehrungsrechner
{
    public partial class ViewController : UIViewController
    {
        List<Conversion> conversionList = new List<Conversion>();

        public ViewController (IntPtr handle) : base (handle)
        {
        }

        public override void ViewDidLoad ()
        {
            base.ViewDidLoad ();
            // Perform any additional setup after loading the view, typically from a nib.
            Utility utility = new Utility();
            conversionList = utility.jsonGetQuotes("EUR");
        }

        public override void DidReceiveMemoryWarning ()
        {
            base.DidReceiveMemoryWarning ();
            // Release any cached data, images, etc that aren't in use.
        }
    }
}