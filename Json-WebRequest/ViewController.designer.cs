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

namespace Json_WebRequest
{
    [Register ("ViewController")]
    partial class ViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel lblAutor { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel lblZitat { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (lblAutor != null) {
                lblAutor.Dispose ();
                lblAutor = null;
            }

            if (lblZitat != null) {
                lblZitat.Dispose ();
                lblZitat = null;
            }
        }
    }
}