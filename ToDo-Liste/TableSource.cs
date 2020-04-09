using System;
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


        //Konstrutkor mit Übergabe der TaskList aus dem ViewController
        public TableSource(List<Task> taskList)
        {
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
                cell = new UITableViewCell(UITableViewCellStyle.Default, cellIdentifier);
            }
            //Wir holen uns das entsprechende Item aus der TaskList und schreiben es in die Zelle
            cell.TextLabel.Text = taskList[rowIndex].Name;
            //Wir ändern die Textfarbe
            cell.TextLabel.TextColor = UIColor.FromRGB(128, 0, 128);
            //wir ändern die Schriftart und -größe
            cell.TextLabel.Font = UIFont.FromName("Chalkduster", 20);

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
            }
        }
    }
}