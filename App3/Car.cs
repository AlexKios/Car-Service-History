using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SQLite;
using System.IO;

namespace App3
{
    [Table("Items")]
    public class Car
    {
        [PrimaryKey, AutoIncrement, Column("_id")]
        public int Id { get; set; }
        public string Model { get; set; }
        public string Year { get; set; }
        public string Engine { get; set; }
        public string Tyre { get; set; }
        public string Km { get; set; }
        public string KmOfOil { get; set; }
        public string DateOfOil { get; set; }

    }
}