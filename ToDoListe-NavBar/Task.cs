using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using UIKit;

namespace ToDoListe_NavBar
{
    public class Task
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public bool IsDone { get; set; }
        public DateTime CreateDate { get; set; }

        //Standardkonstruktor
        public Task()
        {
        }

        //Überladener Konstruktor
        //gedacht wenn neuer Task hinzugefügt wird
        public Task(string taskName)
        {
            Name = taskName;
            IsDone = false;
            CreateDate = DateTime.Now.Date;
        }
    }
}