using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Foundation;
using SQLite;
using UIKit;

namespace ToDoListe_NavBar
{
    public class DBHelper
    {
        static string DBName = "ToDoList.db3";
        string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), DBName);

        SQLiteConnection db;

        //Erstellen einer Datenbank
        public void CreateDB()
        {
            //Wenn Datenbankdatei noch nicht existiert
            if (!File.Exists(dbPath))
            {
                //Baue eine Verbindung auf
                db = new SQLiteConnection(dbPath);
                db.CreateTable<Task>();
                db.Close();
            }
        }

        //Hinzufügen eines Tasks in die Datenbank
        public void addTask(Task task)
        {
            db = new SQLiteConnection(dbPath);
            db.Insert(task);
            db.Close();
        }

        //Löschen eines Tasks in der Datenbank
        public void deleteTask(Task task)
        {
            db = new SQLiteConnection(dbPath);
            //db.Execute<Task>("DELETE * FROM Task WHERE ID = ?", task.ID);
            db.Delete(task);
            db.Close();


        }

        //Aktualisieren des Tasks in der Datenbank
        public void updateTask(Task task)
        {
            db = new SQLiteConnection(dbPath);
            db.Update(task);
            db.Close();
        }

        //Lösche alle Tasks
        public void deleteAllTasks()
        {
            db = new SQLiteConnection(dbPath);
            db.DeleteAll<Task>();
            db.Close();
        }

        //Alle Tasks aus der DB in Liste schreiben
        public List<Task> getAllTask()
        {
            List<Task> taskList = new List<Task>();
            db = new SQLiteConnection(dbPath);
            taskList = db.Query<Task>("SELECT * FROM Task");
            db.Close();
            return taskList;
        }

        //Ein bestimmten Task aus der DB holen
        public Task getTask(int index)
        {
            Task newTask = new Task();
            db = new SQLiteConnection(dbPath);
            newTask = db.Get<Task>(index);
            db.Close();
            return newTask;

            //LINQ-Schreibweise
            /*var linqTask = from t in db.Table<Task>()
                      where t.ID.Equals(index)
                      select t;
            return linqTask.FirstOrDefault();*/
        }
    }
}