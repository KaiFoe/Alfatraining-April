// WARNING
//
// This file has been generated automatically by Visual Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;

namespace PhoneTranslator
{
    [Register ("ViewController")]
    partial class ViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton btnCall { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton btnCallHistory { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton btnTranslate { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel lblPhoneWord { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField txtPhoneWord { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (btnCall != null) {
                btnCall.Dispose ();
                btnCall = null;
            }

            if (btnCallHistory != null) {
                btnCallHistory.Dispose ();
                btnCallHistory = null;
            }

            if (btnTranslate != null) {
                btnTranslate.Dispose ();
                btnTranslate = null;
            }

            if (lblPhoneWord != null) {
                lblPhoneWord.Dispose ();
                lblPhoneWord = null;
            }

            if (txtPhoneWord != null) {
                txtPhoneWord.Dispose ();
                txtPhoneWord = null;
            }
        }
    }
}