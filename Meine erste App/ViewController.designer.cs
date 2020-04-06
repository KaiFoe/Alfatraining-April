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

namespace Meine_erste_App
{
    [Register ("ViewController")]
    partial class ViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton btnSend { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel lblOutput { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField txtInputName { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (btnSend != null) {
                btnSend.Dispose ();
                btnSend = null;
            }

            if (lblOutput != null) {
                lblOutput.Dispose ();
                lblOutput = null;
            }

            if (txtInputName != null) {
                txtInputName.Dispose ();
                txtInputName = null;
            }
        }
    }
}