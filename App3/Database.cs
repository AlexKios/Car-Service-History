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
    public class Database
    {
        SQLiteConnection _db;
        public Database()
        {
            string dbPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal),"cars.db3");
            var db = new SQLiteConnection(dbPath);
            db.CreateTable<Car>();
            _db = db;
        }

        public void Insert(Car car)
        {
            _db.Insert(car);
        }
        public void Delete(Car car)
        {
            _db.Delete(car);
        }
        public void Update(Car car)
        {
            _db.Update(car);
        }
        public TableQuery<Car> GetTable()
        {
            return _db.Table<Car>();
        }
        public bool IsDatabaseEmpty()
        {
            if (_db.Table<Note>() == null)
            {
                return true;
            }
            else
            {
                if (_db.Table<Note>().Count() == 0)
                {
                    return true;
                }
                return false;
            }
        }
    }
}