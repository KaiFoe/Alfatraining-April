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
        public QuotesViewController (IntPtr handle) : base (handle)
        {
            quotesList = new List<Quote>();
            myrequest = new MyRequest();
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            quotesList = myrequest.jsonGetQuotes("15", "0");
            tableQuotes.Source = new TableSource(quotesList, this);
            tableQuotes.ReloadData();
        }
    }
}