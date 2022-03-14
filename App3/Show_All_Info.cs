using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AndroidX.AppCompat.App;
using AndroidX.Core.View;
using AndroidX.DrawerLayout.Widget;
using Google.Android.Material.Navigation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace App3
{
    [Activity(Label = "All Notes", Theme = "@style/AppTheme.NoActionBar")]
    public class Show_All_Info : AppCompatActivity, NavigationView.IOnNavigationItemSelectedListener
    {
        Database cars = new Database();
        DataBaseNotes dataBaseNotes = new DataBaseNotes();
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);

            SetContentView(Resource.Layout.show_all_info);
            AndroidX.AppCompat.Widget.Toolbar toolbar = FindViewById<AndroidX.AppCompat.Widget.Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);

            DrawerLayout drawer = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
            ActionBarDrawerToggle toggle = new ActionBarDrawerToggle(this, drawer, toolbar, Resource.String.navigation_drawer_open, Resource.String.navigation_drawer_close);
            drawer.AddDrawerListener(toggle);
            toggle.SyncState();

            NavigationView navigationView = FindViewById<NavigationView>(Resource.Id.nav_view);
            navigationView.SetNavigationItemSelectedListener(this);

            var showName = FindViewById<TextView>(Resource.Id.carName);
            var cars1 = cars.GetTable().ToList();
            var allNotes = dataBaseNotes.GetTable().ToList();
            var notes = allNotes[Choose_Car.GetId()].GetNotes().ToList();
            showName.Text = cars1[Choose_Car.GetId()].Model+" "+ cars1[Choose_Car.GetId()].Year;

            var tableLayout = FindViewById<TableLayout>(Resource.Id.tableLayout1);
            foreach (var note in notes)
            {
                if (note == null)
                {

                }
                else
                {
                    var tableRow = new TableRow(this);
                    tableLayout.AddView(tableRow);

                    var emptyRow1 = new TableRow(this);
                    tableLayout.AddView(emptyRow1);
                    var emptyView1 = new TextView(this);
                    emptyView1.Text = "\n\n";
                    tableRow.AddView(emptyView1);

                    var textView = new TextView(this);

                    textView.Text = note.Date + " " + note.Km +" km"+ " \n" + note.Text;
                    textView.SetTextColor(Color.Black);
                    textView.SetTextSize(global::Android.Util.ComplexUnitType.Dip, 25);
                    tableRow.AddView(textView);

                    var emptyRow = new TableRow(this);
                    tableLayout.AddView(emptyRow);
                    var emptyView = new TextView(this);
                    emptyView.Text = "\n\n\n\n";
                    tableRow.AddView(emptyView);
                }
                
            }
        }

        public override void OnBackPressed()
        {
            DrawerLayout drawer = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
            if (drawer.IsDrawerOpen(GravityCompat.Start))
            {
                drawer.CloseDrawer(GravityCompat.Start);
            }
            else
            {
                base.OnBackPressed();
            }
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.menu_main, menu);
            return true;
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            int id = item.ItemId;
            if (id == Resource.Id.action_settings)
            {
                var intent = new Intent(this, typeof(Choose_Car));

                this.StartActivity(intent);
            }

            return base.OnOptionsItemSelected(item);
        }


        public bool OnNavigationItemSelected(IMenuItem item)
        {
            int id = item.ItemId;

            if (id == Resource.Id.nav_camera)
            {
                var intent = new Intent(this, typeof(MainActivity));

                this.StartActivity(intent);
            }
            else if (id == Resource.Id.nav_manage)
            {
                var intent = new Intent(this, typeof(Show_More_Info));

                this.StartActivity(intent);
            }
            else if (id == Resource.Id.nav_add)
            {
                var intent = new Intent(this, typeof(Add_Car));

                this.StartActivity(intent);
            }
          
            DrawerLayout drawer = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
            drawer.CloseDrawer(GravityCompat.Start);
            return true;
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}