using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace App3
{
    public class DataBaseNotes
    {
        SQLiteConnection _db;
        public DataBaseNotes()
        {
            string dbPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "allNotes.db3");
            var db = new SQLiteConnection(dbPath);
            db.CreateTable<AllNotes>();
            _db = db;
        }
        public void Insert(AllNotes allNotes)
        {
            _db.Insert(allNotes);
        }
        public void Delete(AllNotes allNotes)
        {
            _db.Delete(allNotes);
        }
        public void Update(AllNotes allNotes)
        {
            _db.Update(allNotes);
        }
        public TableQuery<AllNotes> GetTable()
        {
            return _db.Table<AllNotes>();
        }
        public bool IsDatabaseNoteEmpty()
        {
            if (_db.Table<AllNotes>() == null)
            {
                return true;
            }
            else
            {
                if (_db.Table<AllNotes>().Count() == 0)
                {
                    if (_db.Table<AllNotes>().ToList()[0] == null)
                    {
                        return true;
                    }
                    return false;
                }
                else if (_db.Table<AllNotes>().Count() >= 1) 
                {
                    var list = _db.Table<AllNotes>().ToList();
                    if (list[Choose_Car.GetId()].IsNotesEmpty())
                    {
                        return true;
                    }
                    return false;
                }
                return true;
            }
        }
    }
}