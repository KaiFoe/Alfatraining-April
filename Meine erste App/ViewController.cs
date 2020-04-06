using Foundation;
using System;
using UIKit;

namespace Meine_erste_App
{
    public partial class ViewController : UIViewController
    {
        public ViewController (IntPtr handle) : base (handle)
        {
        }

        public override void ViewDidLoad ()
        {
            base.ViewDidLoad ();

            //btnSend.TouchUpInside += BtnSend_TouchUpInside;
            btnSend.TouchUpInside += (object sender, EventArgs e) =>
            {
                lblOutput.Text = "Hallo" + txtInputName.Text;
            };
        }

        private void BtnSend_TouchUpInside(object sender, EventArgs e)
        {
            lblOutput.Text = "Hallo" + txtInputName.Text;
        }

        public override void DidReceiveMemoryWarning ()
        {
            base.DidReceiveMemoryWarning ();
            // Release any cached data, images, etc that aren't in use.
        }
    }
}