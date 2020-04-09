using Foundation;
using System;
using System.Collections.Generic;
using UIKit;

namespace ToDo_Liste
{
    public partial class ViewController : UIViewController
    {
        //Deklaration der TaskList
        List<string> taskList { get; set; }

        //Standardkonstruktor des ViewController
        public ViewController (IntPtr handle) : base (handle)
        {
        }

        //ViewDidLoad-Methode (wird beim instanzieren der View aufgerufen)
        public override void ViewDidLoad ()
        {
            base.ViewDidLoad ();
            
            //Initialisieren der TaskList
            taskList = new List<string>();
            //Verbinden der TableSource mit unserer TableView "tableTasks"
            tableTasks.Source = new TableSource(taskList);
            //Hinzufügen der TableView "tableTasks" zu unserer View
            Add(tableTasks);

            //ausgelagertes TouchUpInside-Event unseres Add-Buttons 
            btnAdd.TouchUpInside += BtnAdd_TouchUpInside;
        }


        private void BtnAdd_TouchUpInside(object sender, EventArgs e)
        {
            //Wenn Textfeld nicht leer, dann...
            if (!String.IsNullOrWhiteSpace(txtInputTask.Text))
            {
                //... füge Task der Liste hinzu
                taskList.Add(txtInputTask.Text);
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