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

        //Standardkonstruktor des ViewController
        public ViewController (IntPtr handle) : base (handle)
        {
        }

        //ViewDidLoad-Methode (wird beim instanzieren der View aufgerufen)
        public override void ViewDidLoad ()
        {
            base.ViewDidLoad ();
            
            //Initialisieren der TaskList
            taskList = new List<Task>();
            //Verbinden der TableSource mit unserer TableView "tableTasks"
            tableTasks.Source = new TableSource(taskList);

            //ausgelagertes TouchUpInside-Event unseres Add-Buttons 
            btnAdd.TouchUpInside += BtnAdd_TouchUpInside;
        }


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

        public override void DidReceiveMemoryWarning ()
        {
            base.DidReceiveMemoryWarning ();
            // Release any cached data, images, etc that aren't in use.
        }
    }
}