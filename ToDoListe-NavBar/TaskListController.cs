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

        public TaskListController (IntPtr handle) : base (handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

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
        }

        public void deleteAll(object sender, EventArgs args)
        {
            taskList.Clear();
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