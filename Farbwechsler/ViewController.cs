using Foundation;
using System;
using UIKit;

namespace Farbwechsler
{
    public partial class ViewController : UIViewController
    {
        public ViewController (IntPtr handle) : base (handle)
        {
        }

        public override void ViewDidLoad ()
        {
            base.ViewDidLoad ();

            sldrBlue.ValueChanged += sldrChange;
            sldrGreen.ValueChanged += sldrChange;
            sldrRed.ValueChanged += sldrChange;

            txtBlue.ValueChanged += txtChange;
            txtGreen.ValueChanged += txtChange;
            txtRed.ValueChanged += txtChange;
        }

        private void txtChange(object sender, EventArgs e)
        {
            sldrBlue.Value = int.Parse(txtBlue.Text);
            sldrGreen.Value = int.Parse(txtGreen.Text);
            sldrRed.Value = int.Parse(txtRed.Text);
        }

        private void sldrChange(object sender, EventArgs e)
        {
            colorChange((int)sldrRed.Value, (int)sldrGreen.Value, (int)sldrBlue.Value);
            txtBlue.Text = sldrBlue.Value.ToString();
            txtGreen.Text = sldrGreen.Value.ToString();
            txtRed.Text = sldrRed.Value.ToString();
        }

        public override void DidReceiveMemoryWarning ()
        {
            base.DidReceiveMemoryWarning ();
            // Release any cached data, images, etc that aren't in use.
        }

        private void colorChange(int red, int green, int blue)
        {
            lblOutput.BackgroundColor = UIColor.FromRGB(red, green, blue);
            string hexred = red.ToString("X2");
            string hexgreen = green.ToString("X2");
            string hexblue = blue.ToString("X2");
            lblOutput.Text = "Farbcode: #" + hexred + hexgreen + hexblue;
            lblOutput.TextColor = UIColor.FromRGB(Invert(red), Invert(green), Invert(blue));
        }

        private static int Invert(int color)
        {
            return 255 - color;
        }
    }
}