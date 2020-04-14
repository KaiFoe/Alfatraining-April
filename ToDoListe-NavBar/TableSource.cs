using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using UIKit;

namespace ToDoListe_NavBar
{
    class TableSource : UITableViewSource
    {
        //Deklaration und Initialisierung der benötigten Felder
        List<Task> taskList = new List<Task>();
        string cellIdentifier = "TableCell";
        TaskListController taskListController;


        //Konstrutkor mit Übergabe der TaskList aus dem ViewController
        public TableSource(TaskListController controller, List<Task> taskList)
        {
            taskListController = controller;
            //Übergabe der TaskList aus dem ViewController an unsere eigene TaskList
            this.taskList = taskList;
        }

        //Rückgabe-Methode der Cell
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
                cell = new UITableViewCell(UITableViewCellStyle.Value1, cellIdentifier);
            }
            //Wir holen uns das entsprechende Item aus der TaskList und schreiben es in die Zelle
            cell.TextLabel.Text = taskList[rowIndex].Name;
            //Trage Create-Date ein, wenn TaskName vorhanden
            if (!string.IsNullOrWhiteSpace(cell.TextLabel.Text))
                cell.DetailTextLabel.Text = taskList[rowIndex].CreateDate.ToString();

            //Abfragen ob TaskObjekt als ereldigt markiert
            if (taskList[rowIndex].IsDone)
            {
                cell.Accessory = UITableViewCellAccessory.Checkmark;
                cell.TextLabel.TextColor = UIColor.FromRGB(123, 123, 123);
            }
            else
            {
                //Wir ändern die Textfarbe
                cell.TextLabel.TextColor = UIColor.FromRGB(128, 0, 128);
                //wir ändern die Schriftart und -größe
                cell.TextLabel.Font = UIFont.FromName("Chalkduster", 20);
                cell.Accessory = UITableViewCellAccessory.None;
            }

            //wir geben die Zelle zurück
            return cell;
        }

        public override nint RowsInSection(UITableView tableview, nint section)
        {
            return taskList.Count;
        }

        //ContextualAction zum Löschen des TaskItems
        public UIContextualAction contextualDeleteAction(NSIndexPath indexPath, UITableView tableView)
        {
            var action = UIContextualAction.FromContextualActionStyle(UIContextualActionStyle.Normal,
                "Delete Task",
                (UIContextualAction DeleteItem, UIView view, UIContextualActionCompletionHandler success) => {
                    var alertController = UIAlertController.Create("Lösche Task?", "Möchten Sie den Task löschen?", UIAlertControllerStyle.Alert);
                    alertController.AddAction(UIAlertAction.Create("Abbrechen", UIAlertActionStyle.Cancel, null));
                    alertController.AddAction(UIAlertAction.Create("OK", UIAlertActionStyle.Default, onClick =>
                    {
                        tableView.BeginUpdates();
                        taskList.RemoveAt(indexPath.Row);
                        tableView.DeleteRows(new NSIndexPath[] { indexPath }, UITableViewRowAnimation.Fade);
                        tableView.EndUpdates();
                    }));
                    taskListController.PresentViewController(alertController, true, null);
                    success(true);
            });
            return action;
        }

        //ContextualAction zum Bearbeiten des TaskItems
        public UIContextualAction contextualEditAction(int row, UITableView tableView)
        {
            var action = UIContextualAction.FromContextualActionStyle(UIContextualActionStyle.Normal,
                "Edit",
                (UIContextualAction EditItem, UIView view, UIContextualActionCompletionHandler success) =>
                {
                    var alertController = UIAlertController.Create("Editiere Task", "Bitte neue Taskbeschreibung angeben", UIAlertControllerStyle.Alert);

                    UITextField EditTask = null;
                    alertController.AddTextField(EditTaskTxt =>
                   {
                       EditTask = EditTaskTxt;
                       EditTask.Text = taskList[row].Name;
                   });
                    alertController.AddAction(UIAlertAction.Create("OK",
                        UIAlertActionStyle.Default,
                        onClick =>
                        {
                            taskList[row].Name = EditTask.Text;
                            tableView.BeginUpdates();
                            tableView.ReloadRows(tableView.IndexPathsForVisibleRows, UITableViewRowAnimation.Automatic);
                            tableView.EndUpdates();
                        }));
                    alertController.AddAction(UIAlertAction.Create("Abbrechen",
                        UIAlertActionStyle.Default,
                        onClick =>
                        {
                        }));
                    taskListController.PresentViewController(alertController, true, null);
                    success(true);
                });
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

        public override UISwipeActionsConfiguration GetTrailingSwipeActionsConfiguration(UITableView tableView, NSIndexPath indexPath)
        {
            var editAction = contextualEditAction(indexPath.Row, tableView);
            var deleteAction = contextualDeleteAction(indexPath, tableView);

            var leadingSwipe = UISwipeActionsConfiguration.FromActions(new UIContextualAction[] { editAction, deleteAction });
            leadingSwipe.PerformsFirstActionWithFullSwipe = false;

            return leadingSwipe;
        }
    }
}