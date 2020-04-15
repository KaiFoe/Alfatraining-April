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

namespace Personenverwaltung
{
    [Register ("TablePersonController")]
    partial class TablePersonController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITableView tvPersons { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (tvPersons != null) {
                tvPersons.Dispose ();
                tvPersons = null;
            }
        }
    }
}