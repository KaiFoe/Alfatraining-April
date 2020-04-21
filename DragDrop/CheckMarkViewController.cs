using Foundation;
using System;
using UIKit;

namespace DragDrop
{
    public partial class CheckMarkViewController : UIViewController
    {
        private bool isChecked = false;
        private CheckMarkGestureRecognizer checkMarkGesture;

        public CheckMarkViewController (IntPtr handle) : base (handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            WireUpCheckMarkGestureRecognizer();
        }
        private void WireUpCheckMarkGestureRecognizer()
        {
            checkMarkGesture = new CheckMarkGestureRecognizer();
            checkMarkGesture.AddTarget(() =>
            {
                if (checkMarkGesture.State == (UIGestureRecognizerState.Recognized | UIGestureRecognizerState.Ended))
                {
                    if (isChecked)
                    {
                        imgCheck.Image = UIImage.FromBundle("CheckBox_Unchecked.png");
                    } else
                    {
                        imgCheck.Image = UIImage.FromBundle("CheckBox_Checked.png");
                    }
                    isChecked = !isChecked;
                }
            });
            View.AddGestureRecognizer(checkMarkGesture);
        }
    }
}