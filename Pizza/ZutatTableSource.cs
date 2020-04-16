using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using UIKit;

namespace Pizza
{
    public class ZutatTableSource : UITableViewSource
    {
        List<Zutat> zutatList;
        ZutatViewController zutatViewController;
        string cellIdentifier = "zutatCell";

        public ZutatTableSource(ZutatViewController zutatViewController, List<Zutat> zutatList)
        {
            this.zutatViewController = zutatViewController;
            this.zutatList = zutatList;
        }
        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            //Wir lassen uns die Zeile zurückgeben
            int rowIndex = indexPath.Row;

            //Wir lassen uns eine Zelle geben
            UITableViewCell cell = tableView.DequeueReusableCell(cellIdentifier);
            //Wenn die Zelle leer ist...
            if (cell == null)
            {
                //...legen wir eine neue Zelle an
                cell = new UITableViewCell(UITableViewCellStyle.Default, cellIdentifier);
            }
            //Wir holen uns das entsprechende Item aus der Liste und schreiben es in die Zelle
            cell.TextLabel.Text = zutatList[rowIndex].name;

            //wir geben die Zelle zurück
            return cell;
        }


        public override nint RowsInSection(UITableView tableview, nint section)
        {
            return zutatList.Count();
        }

        //ContextualAction zum Löschen des Items
        public UIContextualAction contextualDeleteAction(NSIndexPath indexPath, UITableView tableView)
        {
            var action = UIContextualAction.FromContextualActionStyle(UIContextualActionStyle.Normal,
                "Delete Pizza",
                (UIContextualAction DeleteItem, UIView view, UIContextualActionCompletionHandler success) => {
                    var alertController = UIAlertController.Create("Lösche Pizza?", "Möchten Sie die Pizza entfernen?", UIAlertControllerStyle.Alert);
                    alertController.AddAction(UIAlertAction.Create("Abbrechen", UIAlertActionStyle.Cancel, null));
                    alertController.AddAction(UIAlertAction.Create("OK", UIAlertActionStyle.Default, onClick =>
                    {
                        tableView.BeginUpdates();
                        zutatList.RemoveAt(indexPath.Row);
                        tableView.DeleteRows(new NSIndexPath[] { indexPath }, UITableViewRowAnimation.Fade);
                        tableView.EndUpdates();
                    }));
                    zutatViewController.PresentViewController(alertController, true, null);
                    success(true);
                });
            action.BackgroundColor = UIColor.Red;
            return action;
        }

        //ContextualAction zum Bearbeiten des Namens
        public UIContextualAction contextualEditAction(int row, UITableView tableView)
        {
            var action = UIContextualAction.FromContextualActionStyle(UIContextualActionStyle.Normal,
                "Edit",
                (UIContextualAction EditItem, UIView view, UIContextualActionCompletionHandler success) =>
                {
                    var alertController = UIAlertController.Create("Editiere Pizza", "Bitte neuen Pizzanamen angeben", UIAlertControllerStyle.Alert);

                    UITextField EditTask = null;
                    alertController.AddTextField(EditTaskTxt =>
                    {
                        EditTask = EditTaskTxt;
                        EditTask.Text = zutatList[row].name;
                    });
                    alertController.AddAction(UIAlertAction.Create("OK",
                        UIAlertActionStyle.Default,
                        onClick =>
                        {
                            zutatList[row].name = EditTask.Text;
                            tableView.BeginUpdates();
                            tableView.ReloadRows(tableView.IndexPathsForVisibleRows, UITableViewRowAnimation.Automatic);
                            tableView.EndUpdates();
                        }));
                    alertController.AddAction(UIAlertAction.Create("Abbrechen",
                        UIAlertActionStyle.Default,
                        onClick =>
                        {
                        }));
                    zutatViewController.PresentViewController(alertController, true, null);
                    success(true);
                });
            action.BackgroundColor = UIColor.Green;
            return action;
        }

        public override UISwipeActionsConfiguration GetLeadingSwipeActionsConfiguration(UITableView tableView, NSIndexPath indexPath)
        {
            var editAction = contextualEditAction(indexPath.Row, tableView);
            var deleteAction = contextualDeleteAction(indexPath, tableView);

            var leadingSwipe = UISwipeActionsConfiguration.FromActions(new UIContextualAction[] { editAction, deleteAction });
            leadingSwipe.PerformsFirstActionWithFullSwipe = false;

            return leadingSwipe;
        }
    }
}