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
    [Register ("PizzaViewController")]
    partial class PizzaViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITableView tablePizza { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (tablePizza != null) {
                tablePizza.Dispose ();
                tablePizza = null;
            }
        }
    }
}