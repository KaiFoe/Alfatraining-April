using Foundation;
using System;
using System.Collections.Generic;
using UIKit;

namespace ToDoListe_NavBar
{
    public partial class TaskListController : UITableViewController
    {
        //Variablendeklaration
        TableSource tableSource;
        List<Task> taskList = new List<Task>();
        DBHelper dbHelper = new DBHelper();

        public TaskListController (IntPtr handle) : base (handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            //Erstelle DB falls noch nicht vorhanden
            dbHelper.CreateDB();

            //Holle alle Tasks aus DB
            taskList = dbHelper.getAllTask();

            //TableSource verbinden
            tableTasks.Source = new TableSource(this, taskList);

            //Navigationsitem zum Hinzufügen eines Tasks
            UIBarButtonItem barbtnAdd = new UIBarButtonItem(UIBarButtonSystemItem.Add, (object sender, EventArgs args) =>
            {
                CreateDialog();
            });

            //Navigationsitem zum leeren der Liste
            UIBarButtonItem barbtnDelete = new UIBarButtonItem(UIBarButtonSystemItem.Trash, deleteAll);

            //Navigationsitems im Array zusammenfassen
            UIBarButtonItem[] buttons = new UIBarButtonItem[] { barbtnAdd, barbtnDelete };
            //Navigationsitems der NavBar hinzufügen
            NavigationItem.SetRightBarButtonItems(buttons, true);

            //LongPress-Gesture zum Editieren des TaskItems
            UILongPressGestureRecognizer longPressGestureRecognizer = new UILongPressGestureRecognizer(LongPress);
            tableTasks.AddGestureRecognizer(longPressGestureRecognizer);
        }

        public void deleteAll(object sender, EventArgs args)
        {
            //Alle Einträge in DB löschen
            dbHelper.deleteAllTasks();
            //Tasklist leeren
            taskList.Clear();
            //TableView neu laden
            tableTasks.ReloadData();
        }


        private void CreateDialog()
        {
            //AlertController anlegen
            UIAlertController alertController = UIAlertController.Create(
                "Task hinzufügen",
                "Bitte Taskbeschreibung angeben",
                UIAlertControllerStyle.Alert);

            //Eingabefeld hinzufügen
            UITextField txtAddTask = null;
            alertController.AddTextField(AddTaskTxt =>
            {
                txtAddTask = AddTaskTxt;
                txtAddTask.Placeholder = "Hier Taskbechreibung einfügen";
            });

            //OK-Button hinzufügen
            alertController.AddAction(UIAlertAction.Create(
                "OK",
                UIAlertActionStyle.Default,
                onClick =>
                {
                    if (txtAddTask.Text.Length > 0)
                    {
                        taskList.Add(new Task(txtAddTask.Text));
                        tableTasks.ReloadData();
                        dbHelper.addTask(new Task(txtAddTask.Text));
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

        //LongPress-Gesture zum bearbeiten von Tasks
        private void LongPress(UILongPressGestureRecognizer longPressGestureRecognizer)
        {
            if (longPressGestureRecognizer.State == UIGestureRecognizerState.Began)
            {
                var point = longPressGestureRecognizer.LocationInView(tableTasks);
                var indexPath = tableTasks.IndexPathForRowAtPoint(point);
                EditItemDialog(indexPath);
            }
        }

        //AlertDialog zum Bearbeiten von einem Task
        private void EditItemDialog(NSIndexPath indexPath)
        {
            Task currentTask = taskList[indexPath.Row];
            //AlertController anlegen
            UIAlertController alertController = UIAlertController.Create(
                "Task bearbeiten",
                "Bitte Taskbeschreibung ändern",
                UIAlertControllerStyle.Alert);

            //Eingabefeld hinzufügen
            UITextField txtEditTask = null;
            alertController.AddTextField(EditTaskTxt =>
            {
                txtEditTask = EditTaskTxt;
                txtEditTask.Text = currentTask.Name;
            });

            //OK-Button hinzufügen
            alertController.AddAction(UIAlertAction.Create(
                "OK",
                UIAlertActionStyle.Default,
                onClick =>
                {
                    if (txtEditTask.Text.Length > 0)
                    {
                        currentTask.Name = txtEditTask.Text;
                        taskList[indexPath.Row].Name = currentTask.Name;

                        dbHelper.updateTask(currentTask);
                        tableTasks.BeginUpdates();
                        tableTasks.ReloadRows(tableTasks.IndexPathsForVisibleRows, UITableViewRowAnimation.Automatic);
                        tableTasks.EndUpdates();
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