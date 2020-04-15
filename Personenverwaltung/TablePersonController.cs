using Foundation;
using System;
using System.Collections.Generic;
using UIKit;

namespace Personenverwaltung
{
    public partial class TablePersonController : UITableViewController
    {
        public static List<Person> listPerson = new List<Person>();

        public TablePersonController (IntPtr handle) : base (handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            tvPersons.Source = new TableSource(this, listPerson);
            tvPersons.ReloadData();

            //Navigations-Buttons anlegen
            UIBarButtonItem btnAdd = new UIBarButtonItem(UIBarButtonSystemItem.Add, btnAdd_onClick);
            UIBarButtonItem btnDelete = new UIBarButtonItem(UIBarButtonSystemItem.Trash, btnDelete_onClick);

            //Buttons der Navigation hinzufügen
            UIBarButtonItem[] buttons = new UIBarButtonItem[] { btnDelete, btnAdd };
            NavigationItem.SetRightBarButtonItems(buttons, true);

            //Swipe-Gesten hinzufügen
            UILongPressGestureRecognizer longPressGestureRecognizer = new UILongPressGestureRecognizer(longPress);
            tvPersons.AddGestureRecognizer(longPressGestureRecognizer);
        }

        private void longPress()
        {
           
        }

        public void btnAdd_onClick(object sender, EventArgs args)
        {
            DetailViewController detailViewController = (DetailViewController) Storyboard.InstantiateViewController("DetailViewController");
            detailViewController.personList = listPerson;
            detailViewController.tablePersonController = this;
            NavigationController.PushViewController(detailViewController, true);
        }

        public void btnDelete_onClick(object sender, EventArgs args)
        {
            listPerson.Clear();
            tvPersons.ReloadData();
        }

        public override void ViewDidAppear(bool animated)
        {
            base.ViewDidAppear(animated);
            tvPersons.ReloadData();
        }
    }
}