using Foundation;
using System;
using System.Collections.Generic;
using UIKit;

namespace Pizza
{
    public partial class ZutatViewController : UITableViewController
    {
        //Variablendeklaration
        ZutatTableSource zutatTableSource;
        List<Zutat> zutatList = new List<Zutat>();
        
        public ZutatViewController (IntPtr handle) : base (handle)
        {
        }


        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            //TableSource verbinden
            tableZutat.Source = new ZutatTableSource(this, zutatList);

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
            zutatList.Clear();
            tableZutat.ReloadData();
        }

        private void CreateDialog()
        {
            //AlertController anlegen
            UIAlertController alertController = UIAlertController.Create(
                "Zutat hinzufügen",
                "Bitte Zutat-Namen angeben",
                UIAlertControllerStyle.Alert);

            //Eingabefeld hinzufügen
            UITextField txtAddTask = null;
            alertController.AddTextField(AddTaskTxt =>
            {
                txtAddTask = AddTaskTxt;
                txtAddTask.Placeholder = "Hier Zutatnamen einfügen";
            });

            //OK-Button hinzufügen
            alertController.AddAction(UIAlertAction.Create(
                "OK",
                UIAlertActionStyle.Default,
                onClick =>
                {
                    if (txtAddTask.Text.Length > 0)
                    {
                        zutatList.Add(new Zutat(txtAddTask.Text));
                        tableZutat.ReloadData();
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