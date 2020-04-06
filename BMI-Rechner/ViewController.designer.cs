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

namespace BMI_Rechner
{
    [Register ("ViewController")]
    partial class ViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton btnSubmit { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel lblFemale { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel lblHeight { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel lblMale { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel lblOutput { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel lblTitle { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel lblWeight { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UISlider sldrHeight { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UISlider sldrWeight { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (btnSubmit != null) {
                btnSubmit.Dispose ();
                btnSubmit = null;
            }

            if (lblFemale != null) {
                lblFemale.Dispose ();
                lblFemale = null;
            }

            if (lblHeight != null) {
                lblHeight.Dispose ();
                lblHeight = null;
            }

            if (lblMale != null) {
                lblMale.Dispose ();
                lblMale = null;
            }

            if (lblOutput != null) {
                lblOutput.Dispose ();
                lblOutput = null;
            }

            if (lblTitle != null) {
                lblTitle.Dispose ();
                lblTitle = null;
            }

            if (lblWeight != null) {
                lblWeight.Dispose ();
                lblWeight = null;
            }

            if (sldrHeight != null) {
                sldrHeight.Dispose ();
                sldrHeight = null;
            }

            if (sldrWeight != null) {
                sldrWeight.Dispose ();
                sldrWeight = null;
            }
        }
    }
}