using Foundation;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using UIKit;

namespace Json_WebRequest
{
    public partial class ViewController : UIViewController
    {
        public Quote quote;
        public ViewController (IntPtr handle) : base (handle)
        {
            quote = new Quote();
        }

        public override void ViewDidLoad ()
        {
            base.ViewDidLoad ();

            lblAutor.Text = quote.author;
            lblZitat.Text = quote.text;
        }


        public override void DidReceiveMemoryWarning ()
        {
            base.DidReceiveMemoryWarning ();
            // Release any cached data, images, etc that aren't in use.
        }
    }
}