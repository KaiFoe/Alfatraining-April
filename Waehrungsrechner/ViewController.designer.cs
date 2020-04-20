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

namespace Waehrungsrechner
{
    [Register ("ViewController")]
    partial class ViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton btnCalculate { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel lblBetrag { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel lblVon { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel lblZu { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField txtBetrag { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField txtStartWaehrung { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField txtZiel { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (btnCalculate != null) {
                btnCalculate.Dispose ();
                btnCalculate = null;
            }

            if (lblBetrag != null) {
                lblBetrag.Dispose ();
                lblBetrag = null;
            }

            if (lblVon != null) {
                lblVon.Dispose ();
                lblVon = null;
            }

            if (lblZu != null) {
                lblZu.Dispose ();
                lblZu = null;
            }

            if (txtBetrag != null) {
                txtBetrag.Dispose ();
                txtBetrag = null;
            }

            if (txtStartWaehrung != null) {
                txtStartWaehrung.Dispose ();
                txtStartWaehrung = null;
            }

            if (txtZiel != null) {
                txtZiel.Dispose ();
                txtZiel = null;
            }
        }
    }
}