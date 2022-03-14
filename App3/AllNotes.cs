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
    [Table("Items")]
    public class AllNotes
    {
        [PrimaryKey, AutoIncrement, Column("_id")]
        public int Id { get; set; }

        public SQLiteConnection _Notes;
        Database cars = new Database();
        public AllNotes()
        {
            var car = cars.GetTable().ToList()[Choose_Car.GetId()];
            string dbPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), car.Id + "notes.db3");
            var Notes = new SQLiteConnection(dbPath);
            Notes.CreateTable<Note>();
            _Notes = Notes;
        }

        public void InsertNote(Note note)
        {
            _Notes.Insert(note);
        }
        public void DeleteNote(Note note)
        {
            _Notes.Delete(note);
        }
        public void UpdateNote(Note note)
        {
            _Notes.Update(note);
        }
        public TableQuery<Note> GetNotes()
        {
            return _Notes.Table<Note>();
        }
        public void DeleteAll()
        {
            _Notes.DeleteAll<Note>();
        }
        public bool IsNotesEmpty()
        {
            if (_Notes.Table<Note>() == null)
            {
                return true;
            }
            else
            {
                if (_Notes.Table<Note>().Count() == 0)
                {
                    return true;
                }
                return false;
            }
        }
    }
}