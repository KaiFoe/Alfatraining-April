using Foundation;
using System;
using System.Collections.Generic;
using UIKit;

namespace Json_WebRequest
{
    public partial class QuotesViewController : UITableViewController
    {
        List<Quote> quotesList;
        MyRequest myrequest;
        string count = "0";

        public QuotesViewController (IntPtr handle) : base (handle)
        {
            quotesList = new List<Quote>();
            myrequest = new MyRequest();
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            
            quotesList = myrequest.jsonGetQuotes(count, "0");
            tableQuotes.Source = new TableSource(quotesList, this);
            tableQuotes.ReloadData();

            UIBarButtonItem btnRefresh = new UIBarButtonItem(UIBarButtonSystemItem.Refresh, (sender, args) =>
            {
                refreshtable();
            });

            UIBarButtonItem btnCount = new UIBarButtonItem(UIImage.FromBundle("gear.png"), UIBarButtonItemStyle.Plain, countDialog);
            
            btnCount.Image = UIImage.FromBundle("gear.png");
            UIBarButtonItem[] buttons = new UIBarButtonItem[] { btnRefresh, btnCount };
            //Navigationsitems der NavBar hinzufügen
            NavigationItem.SetRightBarButtonItems(buttons, true);
        }

        private void refreshtable()
        {
            quotesList = myrequest.jsonGetQuotes(count, "0");
            tableQuotes.Source = new TableSource(quotesList, this);
            tableQuotes.ReloadData();
        }

        private void countDialog(object sender, EventArgs args)
        {
                //AlertController anlegen
                UIAlertController alertController = UIAlertController.Create(
                    "Anzahl Zitate",
                    "Bitte angeben wie viele Zitate ausgegeben werden sollen",
                    UIAlertControllerStyle.Alert);

                //Eingabefeld hinzufügen
                UITextField txtCountQuotes = null;
                alertController.AddTextField(CountQuotesTxt =>
                {
                    txtCountQuotes = CountQuotesTxt;
                });

                //OK-Button hinzufügen
                alertController.AddAction(UIAlertAction.Create(
                    "OK",
                    UIAlertActionStyle.Default,
                    onClick =>
                    {
                        if (txtCountQuotes.Text.Length > 0)
                        {
                            count = txtCountQuotes.Text;
                            refreshtable();
                        }
                    }));
                //Cancel-Button hinzufügen
                alertController.AddAction(UIAlertAction.Create(
                    "Abbrechen",
                    UIAlertActionStyle.Default,
                    null));
                //PresentViewController aufrufen zur Anzeige des Dialoges
                PresentViewController(alertController, true, null);
        }
    }
}