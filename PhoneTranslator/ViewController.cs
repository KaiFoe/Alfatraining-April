using Foundation;
using System;
using System.Collections.Generic;
using UIKit;

namespace PhoneTranslator
{
    public partial class ViewController : UIViewController
    {
        string translatedNumber = "";
        public List<string> PhoneNumbers { get; set; }

        public ViewController (IntPtr handle) : base (handle)
        {
            //Initialisiere Liste von Nummern für die CallHistory
            PhoneNumbers = new List<string>();
        }

        public override void ViewDidLoad ()
        {
            base.ViewDidLoad ();

            btnTranslate.TouchUpInside += BtnTranslate_TouchUpInside;
            btnCall.TouchUpInside += BtnCall_TouchUpInside;
            
        }

        private void BtnCall_TouchUpInside(object sender, EventArgs e)
        {
            //Speichere Nummer in Liste
            PhoneNumbers.Add(translatedNumber);

            //Nutze den URL-Handler mit dem "tel:"-Präfix um die iPhone-Telefon-App zu öffnen
            var url = new NSUrl("tel:" + translatedNumber);

            
            //andernfalls zeige einen Alert-Dialog
            if (!UIApplication.SharedApplication.OpenUrl(url))
            {
                //Alertdialog erstellen
                var alert = UIAlertController.Create("Not supported",
                    "Scheme 'tel:' is not supported on this device",
                    UIAlertControllerStyle.Alert);
                //Action/Button hinzufügen
                alert.AddAction(UIAlertAction.Create("OK", UIAlertActionStyle.Default, null));
                //Alertdialog anzeigen
                PresentViewController(alert, true, null);
            }
        }

        private void BtnTranslate_TouchUpInside(object sender, EventArgs e)
        {
            //Konvertieren der Telefonnummer von Text zur Nummer
            translatedNumber = PhoneTranslator.ToNumber(txtPhoneWord.Text);

            //Blende Tastatur aus
            txtPhoneWord.ResignFirstResponder();

            if (translatedNumber == "")
            {
                btnCall.SetTitle("Call", UIControlState.Normal);
                btnCall.Enabled = false;
            } else
            {
                btnCall.SetTitle("Call " + translatedNumber, UIControlState.Normal);
                btnCall.Enabled = true;
            }
        }

        public override void DidReceiveMemoryWarning ()
        {
            base.DidReceiveMemoryWarning ();
            // Release any cached data, images, etc that aren't in use.
        }

        public override void PrepareForSegue(UIStoryboardSegue segue, NSObject sender)
        {
            base.PrepareForSegue(segue, sender);

            //setze den ViewController, der angezeigt werden soll
            var callHistoryController = segue.DestinationViewController as CallHistoryController;
            //setze die Liste vom CallHistoryController mit der Liste des ViewControllers gleich
            if (callHistoryController != null)
            {
                callHistoryController.PhoneNumbers = PhoneNumbers;
            }
        }
    }
}