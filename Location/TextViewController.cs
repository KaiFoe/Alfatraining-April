using Foundation;
using System;
using UIKit;

namespace Location
{
    public partial class TextViewController : UIViewController
    {
        public TextViewController (IntPtr handle) : base (handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            int test = textView.Text.Length;
            textView.TextStorage.BeginEditing();
            textView.TextStorage.AddAttribute(UIStringAttributeKey.BackgroundColor, UIColor.Green, new NSRange(25, 50));
            textView.TextStorage.AddAttribute(UIStringAttributeKey.ForegroundColor, UIColor.Red, new NSRange(10, 43));

            textView.TextStorage.EndEditing();

        }
    }
}