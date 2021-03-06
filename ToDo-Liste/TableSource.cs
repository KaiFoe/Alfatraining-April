﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using UIKit;

namespace ToDo_Liste
{
    class TableSource : UITableViewSource
    {
        //Deklaration und Initialisierung der benötigten Felder
        List<Task> taskList = new List<Task>();
        string cellIdentifier = "TableCell";
        ViewController viewController;
        bool hasViewedAlert = false;


        //Konstrutkor mit Übergabe der TaskList aus dem ViewController
        public TableSource(ViewController controller, List<Task> taskList)
        {
            viewController = controller;
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
            } else
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

        public override bool CanEditRow(UITableView tableView, NSIndexPath indexPath)
        {
            //Erlaube Editieren der TableView-Rows
            return true;
        }

        public override void CommitEditingStyle(UITableView tableView, UITableViewCellEditingStyle editingStyle, NSIndexPath indexPath)
        {
            switch (editingStyle)
            {
                case UITableViewCellEditingStyle.Delete:
                    taskList.RemoveAt(indexPath.Row);
                    tableView.DeleteRows(new NSIndexPath[] { indexPath }, UITableViewRowAnimation.Fade);
                    break;
                case UITableViewCellEditingStyle.Insert:
                    viewController.alertDialog();
                    break;
            }
        }

        public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
        {
                //geklickten Task ermitteln
                Task currentTask = taskList[indexPath.Row];
                //isDone-Flag ändern
                currentTask.IsDone = !currentTask.IsDone;

                //TableView neu laden
                tableView.ReloadData();
        
        }

        public override UITableViewCellEditingStyle EditingStyleForRow(UITableView tableView, NSIndexPath indexPath)
        {
            if (tableView.Editing)
            {
                if (indexPath.Row == tableView.NumberOfRowsInSection(0) - 1)
                    return UITableViewCellEditingStyle.Insert;
                else
                    return UITableViewCellEditingStyle.Delete;
            }
            else
                return UITableViewCellEditingStyle.None;
        }

        public void WillBeginTableEditing(UITableView tableView)
        {
            tableView.BeginUpdates();
            //Einfügen der "Add new" Zeile an das Ende der TableView
            tableView.InsertRows(new NSIndexPath[] { NSIndexPath.FromRowSection(tableView.NumberOfRowsInSection(0), 0) },
            UITableViewRowAnimation.Fade);

            //Erstelle ein neues Item und füge es ans Ende hinzu
            taskList.Add(new Task());
            tableView.EndUpdates(); //Änderungen anwenden
        }

        public void DidFinishTableEditing(UITableView tableView)
        {
            tableView.BeginUpdates();
            //entfernen der "Add new" Zeile
            taskList.RemoveAt((int)tableView.NumberOfRowsInSection(0) - 1);
            tableView.DeleteRows(new NSIndexPath[] { NSIndexPath.FromRowSection(tableView.NumberOfRowsInSection(0) - 1, 0) },
                UITableViewRowAnimation.Fade);
            tableView.EndUpdates();
        }

        public override UISwipeActionsConfiguration GetLeadingSwipeActionsConfiguration(UITableView tableView, NSIndexPath indexPath)
        {
            //UIContextualActions
            var definitionAction = ContextualDefinitionAction(indexPath.Row);
            var flagAction = ContextualFlagAction(indexPath.Row);

            var leadingSwipe = UISwipeActionsConfiguration.FromActions(new UIContextualAction[]
            {
                flagAction,
                definitionAction
            });

            leadingSwipe.PerformsFirstActionWithFullSwipe = false;
            return leadingSwipe;
        }

        public UIContextualAction ContextualFlagAction(int row)
        {
            var action = UIContextualAction.FromContextualActionStyle
                            (UIContextualActionStyle.Normal,
                                "Flag",
                                (FlagAction, view, success) => {
                                    var alertController = UIAlertController.Create($"Report {taskList[row].Name}?", "", UIAlertControllerStyle.Alert);
                                    alertController.AddAction(UIAlertAction.Create("Cancel", UIAlertActionStyle.Cancel, null));
                                    alertController.AddAction(UIAlertAction.Create("Yes", UIAlertActionStyle.Destructive, null));
                                    viewController.PresentViewController(alertController, true, null);

                                    success(true);
                                });

            //action.Image = UIImage.FromFile("lifecycle.png");
            action.BackgroundColor = UIColor.Blue;

            return action;
        }

        public UIContextualAction ContextualDefinitionAction(int row)
        {
            string word = taskList[row].Name;

            var action = UIContextualAction.FromContextualActionStyle(UIContextualActionStyle.Normal,
                                                                "Definition",
                                                                (ReadLaterAction, view, success) => {
                                                                    var def = new UIReferenceLibraryViewController(word);

                                                                    var alertController = UIAlertController.Create("No Dictionary Installed", "To install a Dictionary, Select Definition again, click `Manage` on the next screen and select a dictionary to download", UIAlertControllerStyle.Alert);
                                                                    alertController.AddAction(UIAlertAction.Create("OK", UIAlertActionStyle.Default, null));

                                                                    if (UIReferenceLibraryViewController.DictionaryHasDefinitionForTerm(word) || hasViewedAlert == true)
                                                                    {
                                                                        viewController.PresentViewController(def, true, null);
                                                                        success(true);
                                                                    }
                                                                    else
                                                                    {
                                                                        viewController.PresentViewController(alertController, true, null);
                                                                        hasViewedAlert = true;
                                                                        success(false);
                                                                    }
                                                                });
            action.BackgroundColor = UIColor.Orange;
            return action;
        }
    }
}