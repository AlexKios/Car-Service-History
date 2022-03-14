using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace App3
{
    [Activity(Label = "Choose Car")]
    public class Choose_Car : Activity
    {
        TableLayout table;
        static int id = 0;
        Database cars = new Database();
        int i = 0;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.choose_car);
            var table1 = cars.GetTable();
            if (cars.IsDatabaseEmpty())
            {
                var intent = new Intent(this, typeof(MainActivity));
                this.StartActivity(intent);
            }
            else
            {
                
                table = FindViewById<TableLayout>(Resource.Id.tableLayout2);

                foreach (var car in table1)
                {
                    var tableRow = new TableRow(this);
                    table.AddView(tableRow);

                    var emptyRow1 = new TableRow(this);
                    table.AddView(emptyRow1);
                    var emptyView1 = new TextView(this);
                    emptyView1.Text = "\n\n";
                    tableRow.AddView(emptyView1);

                    var textView = new TextView(this);

                    textView.Text = car.Model + " " + car.Year;
                    textView.SetTextColor(Color.Black);
                    textView.SetTextSize(global::Android.Util.ComplexUnitType.Dip, 25);
                    var Row1 = new TableRow(this);
                    table.AddView(Row1);
                    tableRow.AddView(textView);

                    var button = new Button(this);
                    button.Text = "Select car";
                    button.Tag = i;
                    button.Click += onClick;
                    table.AddView(button);

                    i++;

                    var emptyRow = new TableRow(this);
                    table.AddView(emptyRow);
                    var emptyView = new TextView(this);
                    emptyView.Text = "\n";

                    tableRow.AddView(emptyView);
                }
            }
        }
        private async void onClick(Object sender, EventArgs e)
        {
            id = Convert.ToInt32(((Button)sender).Tag.ToString());
            var intent = new Intent(this, typeof(MainActivity));
            this.StartActivity(intent);
        }
        public static int GetId()
        {
            return id;
        }
        public static void SetId(int i)
        {
            if (i < 0)
            {
                id = 0;
            }
            else id = i;
        }
        
    }
}