using Foundation;
using System;
using System.Collections.Generic;
using UIKit;

namespace Pizza
{
    public partial class PizzaViewController : UITableViewController
    {
        //Variablendeklaration
        PizzaTableSource pizzaTableSource;
        List<Pizza> pizzaList = new List<Pizza>();

        public PizzaViewController (IntPtr handle) : base (handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            //TableSource verbinden
            tablePizza.Source = new PizzaTableSource(this, pizzaList);

            //Navigationsitem zum Hinzuf�gen eines Tasks
            UIBarButtonItem barbtnAdd = new UIBarButtonItem(UIBarButtonSystemItem.Add, (object sender, EventArgs args) =>
            {
                CreateDialog();
            });

            //Navigationsitem zum leeren der Liste
            UIBarButtonItem barbtnDelete = new UIBarButtonItem(UIBarButtonSystemItem.Trash, deleteAll);

            //Navigationsitems im Array zusammenfassen
            UIBarButtonItem[] buttons = new UIBarButtonItem[] { barbtnAdd, barbtnDelete };
            //Navigationsitems der NavBar hinzuf�gen
            NavigationItem.SetRightBarButtonItems(buttons, true);

        }
        public void deleteAll(object sender, EventArgs args)
        {
            pizzaList.Clear();
            tablePizza.ReloadData();
        }

        private void CreateDialog()
        {
            //AlertController anlegen
            UIAlertController alertController = UIAlertController.Create(
                "Pizza hinzuf�gen",
                "Bitte Pizza-Namen angeben",
                UIAlertControllerStyle.Alert);

            //Eingabefeld hinzuf�gen
            UITextField txtAddTask = null;
            alertController.AddTextField(AddTaskTxt =>
            {
                txtAddTask = AddTaskTxt;
                txtAddTask.Placeholder = "Hier Pizzanamen einf�gen";
            });

            //OK-Button hinzuf�gen
            alertController.AddAction(UIAlertAction.Create(
                "OK",
                UIAlertActionStyle.Default,
                onClick =>
                {
                    if (txtAddTask.Text.Length > 0)
                    {
                        pizzaList.Add(new Pizza(txtAddTask.Text));
                        tablePizza.ReloadData();
                    }
                }));
            //Cancel-Button hinzuf�gen
            alertController.AddAction(UIAlertAction.Create(
                "Abbrechen",
                UIAlertActionStyle.Default,
                null));
            //PresentViewController aufrufen zur Anzeige des Dialoges
            PresentViewController(alertController, true, null);
        }
    }
}