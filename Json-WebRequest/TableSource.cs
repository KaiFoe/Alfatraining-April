using Foundation;
using System;
using System.Collections.Generic;
using UIKit;

namespace Json_WebRequest
{
    internal class TableSource : UITableViewSource
    {
        private List<Quote> quotesList;
        private QuotesViewController quotesViewController;

        public TableSource(List<Quote> quotesList, QuotesViewController quotesViewController)
        {
            this.quotesList = quotesList;
            this.quotesViewController = quotesViewController;
        }

        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            UITableViewCell cell = tableView.DequeueReusableCell("QuoteCell");
            if (cell == null)
                cell = new UITableViewCell(UITableViewCellStyle.Subtitle, "QuoteCell");
            
            cell.TextLabel.Text = quotesList[indexPath.Row].text;
            cell.DetailTextLabel.Text = quotesList[indexPath.Row].author;
            cell.Accessory = UITableViewCellAccessory.DisclosureIndicator;

            return cell;
        }

        public override nint RowsInSection(UITableView tableview, nint section)
        {
            return quotesList.Count;
        }

        public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
        {
            ViewController quoteDetailController = quotesViewController.Storyboard.InstantiateViewController("DetailQuote") as ViewController;
            quoteDetailController.quote = quotesList[indexPath.Row];
            quotesViewController.NavigationController.PushViewController(quoteDetailController, false);
        }
    }
}