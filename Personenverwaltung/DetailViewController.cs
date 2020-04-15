using Foundation;
using System;
using System.Collections.Generic;
using UIKit;

namespace Personenverwaltung
{
    public partial class DetailViewController : UIViewController
    {
        public Person currentPerson;
        public List<Person> personList;
        public TablePersonController tablePersonController;

        public DetailViewController (IntPtr handle) : base (handle)
        {
        }

        public DetailViewController() { }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            if (currentPerson != null)
                fillTextFields(currentPerson);
            

            btnSave.TouchUpInside += BtnSave_TouchUpInside;

        }
        private void fillTextFields(Person currentPerson)
        {
            txtVorname.Text = currentPerson.vorname;
            txtNachname.Text = currentPerson.nachname;
            txtStrasse.Text = currentPerson.strasse;
            txtPLZ.Text = currentPerson.plz;
            txtOrt.Text = currentPerson.ort;
        }

        private void BtnSave_TouchUpInside(object sender, EventArgs e)
        {
            bool newPerson = false;
            //int index = TablePersonController.listPerson.IndexOf(currentPerson);
            if (currentPerson == null)
            {
                currentPerson = new Person();
                newPerson = true;
            }
            currentPerson.vorname = txtVorname.Text;
            currentPerson.nachname = txtNachname.Text;
            currentPerson.strasse = txtStrasse.Text;
            currentPerson.plz = txtPLZ.Text;
            currentPerson.ort = txtOrt.Text;

            if (newPerson)
                personList.Add(currentPerson);

            tablePersonController.NavigationController.PopToRootViewController(true);
            //tablePersonController.NavigationController.PopToViewController("identifier", true);
        }
    }
}