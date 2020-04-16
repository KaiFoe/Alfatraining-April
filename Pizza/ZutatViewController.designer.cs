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

namespace Pizza
{
    [Register ("ZutatViewController")]
    partial class ZutatViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITableView tableZutat { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (tableZutat != null) {
                tableZutat.Dispose ();
                tableZutat = null;
            }
        }
    }
}