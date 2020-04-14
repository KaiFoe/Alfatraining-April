using Foundation;
using System;
using System.Collections.Generic;
using UIKit;

namespace ToDo_Liste
{
    public partial class ViewController : UIViewController
    {
        //Deklaration der TaskList
        List<Task> taskList { get; set; }
        TableSource tableSource;

        //Standardkonstruktor des ViewController
        public ViewController (IntPtr handle) : base (handle)
        {
        }

        //ViewDidLoad-Methode (wird beim instanzieren der View aufgerufen)
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            //Initialisieren der TaskList
            taskList = new List<Task>();
            //Verbinden der TableSource mit unserer TableView "tableTasks"
            tableSource = new TableSource(this, taskList);
            tableTasks.Source = tableSource;

            //ausgelagertes TouchUpInside-Event unseres Add-Buttons 
            btnAdd.TouchUpInside += BtnAdd_TouchUpInside;

            //ausgelagertes TouchUpInside-Event unseres Edit-Buttons
            btnEdit.TouchUpInside += BtnEdit_TouchUpInside;

            //ausgelagertes TouchUpInside-Event unseres Delete-Buttons
            btnDelete.TouchUpInside += BtnDelete_TouchUpInside;

            //LongPress-Gesture der TableView hinzufügen
            UILongPressGestureRecognizer longPressGestureRecognizer = new UILongPressGestureRecognizer(LongPress);
            tableTasks.AddGestureRecognizer(longPressGestureRecognizer);

            //Swipe-Gesture der TableView hinzufügen
            UISwipeGestureRecognizer leftSwipeGesture = new UISwipeGestureRecognizer(SwipeLeftToRight)
            {
                Direction = UISwipeGestureRecognizerDirection.Left
            };
            tableTasks.AddGestureRecognizer(leftSwipeGesture);
        }

        //Tabelle leeren
        private void BtnDelete_TouchUpInside(object sender, EventArgs e)
        {
            //Leere TaskListe
            taskList.Clear();
            //Aktualisiere TableView
            tableTasks.ReloadData();
        }

        //Tabelle bearbeiten
        private void BtnEdit_TouchUpInside(object sender, EventArgs e)
        {
            if (tableTasks.Editing)
            {
                //Bearbeitungsmodus abschalten
                tableTasks.SetEditing(false, false);
                tableSource.DidFinishTableEditing(tableTasks);
                btnAdd.Enabled = true;
            }
            else
            {
                //Bearbeitungsmodus einschalten
                tableSource.WillBeginTableEditing(tableTasks);
                tableTasks.SetEditing(true, true);
                btnAdd.Enabled = false;

            }


            //Vereinfachte/Verkürzte Schreibweise
            //tableTasks.SetEditing(!tableTasks.Editing, !!tableTasks.Editing)
        }

        //Eintrag hinzufügen
        private void BtnAdd_TouchUpInside(object sender, EventArgs e)
        {
            //Wenn Textfeld nicht leer, dann...
            if (!String.IsNullOrWhiteSpace(txtInputTask.Text))
            {
                //...lege neues Task-Objekt an
                Task newTask = new Task(txtInputTask.Text);
                //... füge Task-Objekt der Liste hinzu
                taskList.Add(newTask);
                //...leere Textfeld
                txtInputTask.Text = "";
            }
            //blende Tastatur aus
            txtInputTask.ResignFirstResponder();
            //Lade TableView neu
            tableTasks.ReloadData();
        }

        //AlertDialog zum hinzufügen von einem Task
        public void alertDialog()
        {
            /* Deprecated Version
            UIAlertView alertView = new UIAlertView();
            alertView.AlertViewStyle = UIAlertViewStyle.PlainTextInput;
            alertView.Title = "Neuen Task anlegen";
            alertView.Message = "Bitte Taskbeschreibung angeben";
            alertView.AddButton("Abbrechen");
            alertView.AddButton("OK");
            alertView.Show();

            alertView.Clicked += (object sender, UIButtonEventArgs args) =>
            {
                if (args.ButtonIndex == 1 && alertView.GetTextField(0).Text.Length > 0)
                {
                    taskList.RemoveAt(taskList.Count-1);
                    taskList.Add(new Task(alertView.GetTextField(0).Text));
                    taskList.Add(new Task("(add new)"));
                    tableTasks.ReloadData();
                }
            };*/
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
                        taskList.RemoveAt(taskList.Count - 1);
                        taskList.Add(new Task(txtAddTask.Text));
                        taskList.Add(new Task("(add new)"));
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

        //LongPress-Gesture zum bearbeiten von Tasks
        private void LongPress(UILongPressGestureRecognizer longPressGestureRecognizer)
        {
            if (longPressGestureRecognizer.State == UIGestureRecognizerState.Began)
            {
                var point = longPressGestureRecognizer.LocationInView(tableTasks);
                var indexPath = tableTasks.IndexPathForRowAtPoint(point);
                EditAlertDialog(indexPath);
            }
        }

        //AlertDialog zum Bearbeiten von einem Task
        private void EditAlertDialog(NSIndexPath indexPath)
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
    
        private void SwipeLeftToRight(UISwipeGestureRecognizer swipeGestureRecognizer)
        {
            var point = swipeGestureRecognizer.LocationInView(tableTasks);
            var indexPath = tableTasks.IndexPathForRowAtPoint(point);

            taskList.RemoveAt(indexPath.Row);
            tableTasks.DeleteRows(new NSIndexPath[] { indexPath }, UITableViewRowAnimation.Fade);
        }

        
        public override void DidReceiveMemoryWarning ()
        {
            base.DidReceiveMemoryWarning ();
            // Release any cached data, images, etc that aren't in use.
        }

        
    }
}