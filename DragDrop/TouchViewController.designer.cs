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

namespace DragDrop
{
    [Register ("TouchViewController")]
    partial class TouchViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIImageView imgDrag { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIImageView imgTap { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIImageView imgTouch { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel lblStatus { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIView viewTouch { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (imgDrag != null) {
                imgDrag.Dispose ();
                imgDrag = null;
            }

            if (imgTap != null) {
                imgTap.Dispose ();
                imgTap = null;
            }

            if (imgTouch != null) {
                imgTouch.Dispose ();
                imgTouch = null;
            }

            if (lblStatus != null) {
                lblStatus.Dispose ();
                lblStatus = null;
            }

            if (viewTouch != null) {
                viewTouch.Dispose ();
                viewTouch = null;
            }
        }
    }
}