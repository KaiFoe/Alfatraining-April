using Foundation;
using System;
using UIKit;

namespace BMI_Rechner
{
    public partial class ViewController : UIViewController
    {

        float height;
        float weight;

        public ViewController (IntPtr handle) : base (handle)
        {
        }

        public override void ViewDidLoad ()
        {
            base.ViewDidLoad ();

            txtWeight.Text = sldrWeight.Value.ToString();
            txtHeight.Text = sldrHeight.Value.ToString();

            sldrWeight.ValueChanged += SldrWeight_ValueChanged;
            sldrHeight.ValueChanged += SldrHeight_ValueChanged;

            btnSubmit.TouchUpInside += BtnSubmit_TouchUpInside;

            txtWeight.EditingChanged += TxtWeight_EditingChanged;
            txtHeight.EditingChanged += TxtHeight_EditingChanged;
            //txtHeight.ValueChanged += TxtHeight_EditingChanged;
        }

        private void TxtHeight_EditingChanged(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(txtHeight.Text))
            sldrHeight.Value = float.Parse(txtHeight.Text);
        }

        private void TxtWeight_EditingChanged(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(txtWeight.Text))
                sldrWeight.Value = float.Parse(txtWeight.Text);
        }

        private void BtnSubmit_TouchUpInside(object sender, EventArgs e)
        {
            hideKeyBoard();
            lblOutput.Text = "Your BMI is: " + calcBMI(weight, height);
        }

        private void SldrHeight_ValueChanged(object sender, EventArgs e)
        {
            height = sldrHeight.Value;
            txtHeight.Text = sldrHeight.Value.ToString();
            hideKeyBoard();
        }

        private void SldrWeight_ValueChanged(object sender, EventArgs e)
        {
            weight = sldrWeight.Value;
            txtWeight.Text = sldrWeight.Value.ToString();
            hideKeyBoard();
        }

        public override void DidReceiveMemoryWarning ()
        {
            base.DidReceiveMemoryWarning ();
            // Release any cached data, images, etc that aren't in use.
        }

        private float calcBMI(float weight, float height)
        {
            //height = height / 100;
            //BMI = Gewicht in kg/ (Größe im m)²
            float bmi = weight / (height / 100 * height / 100);

            return bmi;
        }

        private void hideKeyBoard()
        {
            txtHeight.ResignFirstResponder();
            txtWeight.ResignFirstResponder();
        }
    }
}