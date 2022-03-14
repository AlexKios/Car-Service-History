using Android.App;
using Android.Content;
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
    [Activity(Label = "Notes", Theme = "@style/AppTheme.NoActionBar")]
    public class Show_More_Info : AppCompatActivity, NavigationView.IOnNavigationItemSelectedListener
    {
        static int i = 0;
        TextView model, date, km, note;
        Button back, forward, change, add, viewAll, delete;
        Database cars = new Database();
        DataBaseNotes dataBaseNotes = new DataBaseNotes();
        protected override void OnCreate(Bundle savedInstanceState)
        {

            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            
            SetContentView(Resource.Layout.show_more_info);
            AndroidX.AppCompat.Widget.Toolbar toolbar = FindViewById<AndroidX.AppCompat.Widget.Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);

            DrawerLayout drawer = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
            ActionBarDrawerToggle toggle = new ActionBarDrawerToggle(this, drawer, toolbar, Resource.String.navigation_drawer_open, Resource.String.navigation_drawer_close);
            drawer.AddDrawerListener(toggle);
            toggle.SyncState();

            NavigationView navigationView = FindViewById<NavigationView>(Resource.Id.nav_view);
            navigationView.SetNavigationItemSelectedListener(this);
            
            model = FindViewById<TextView>(Resource.Id.noteCarModel);
            date = FindViewById<TextView>(Resource.Id.noteDate);
            km = FindViewById<TextView>(Resource.Id.noteKm);
            note = FindViewById<TextView>(Resource.Id.note);

            back = FindViewById<Button>(Resource.Id.previous);
            back.Click += previousNote;
            forward = FindViewById<Button>(Resource.Id.next);
            forward.Click += nextNote;
            change = FindViewById<Button>(Resource.Id.updateNote);
            change.Click += updateNote;
            add = FindViewById<Button>(Resource.Id.addNewNote);
            add.Click += addNote;
            viewAll = FindViewById<Button>(Resource.Id.viewNotesList);
            viewAll.Click += onViewAllNotes;
            delete = FindViewById<Button>(Resource.Id.deleteNote);
            delete.Click += onDeleteNote;

        }

        #region Button Clicks
        private async void previousNote(Object sender, EventArgs e)
        {
            var allNotes = dataBaseNotes.GetTable().ToList();
            var notes = allNotes[Choose_Car.GetId()].GetNotes().ToList();
            i++;
            if (i > notes.Count-1)
            {
                Toast.MakeText(this, "No older notes", ToastLength.Long).Show();
                i--;
                ShowCurrentNote(i);
            }
            else 
            {
                ShowCurrentNote(i);
            }
        }
        private async void nextNote(Object sender, EventArgs e)
        {
            var allNotes = dataBaseNotes.GetTable().ToList();
            var notes = allNotes[Choose_Car.GetId()].GetNotes().ToList();
            i--;
            if (i < 0)
            {
                Toast.MakeText(this, "No newer notes", ToastLength.Long).Show();
                i++;
                ShowCurrentNote(i);
            }
            else
            {
                ShowCurrentNote(i); 
            }
        }
        private async void addNote(Object sender, EventArgs e)
        {
            var intent = new Intent(this, typeof(Add_More_Info));
            this.StartActivity(intent);
        }
        private async void updateNote(Object sender, EventArgs e)
        {
            var intent = new Intent(this, typeof(Update_More_Info));
            this.StartActivity(intent);
        }
        private async void onDeleteNote(Object sender ,EventArgs e)
        {
            var allNotes = dataBaseNotes.GetTable().ToList();
            var notes = allNotes[Choose_Car.GetId()].GetNotes().ToList();
            
            allNotes[Choose_Car.GetId()].DeleteNote(notes[i]);
            dataBaseNotes.Update(allNotes[Choose_Car.GetId()]);
            
            i--;
            if (i < 0)
            {
                i = 0;
            }
            Toast.MakeText(this, "Note deleted", ToastLength.Short).Show();

            var intent = new Intent(this, typeof(Show_More_Info));
            this.StartActivity(intent);
        }
    
        private async void onViewAllNotes(Object sender, EventArgs e)
        {
            var intent = new Intent(this, typeof(Show_All_Info));
            this.StartActivity(intent);
        }
        #endregion

        protected override void OnStart()
        {
            base.OnStart();

            var car = cars.GetTable().ToList();
            if (cars.IsDatabaseEmpty())
            {
                model.Text = "No car. \n Please add car, then add notes";
                date.Text = "";
                km.Text = "";
                note.Text = "";
                back.Visibility = ViewStates.Invisible;
                forward.Visibility = ViewStates.Invisible;
                change.Visibility = ViewStates.Invisible;
                add.Visibility = ViewStates.Invisible;
                viewAll.Visibility = ViewStates.Invisible;
                delete.Visibility = ViewStates.Invisible;

                var intent = new Intent(this, typeof(Add_Car));
                this.StartActivity(intent);
            }
            else
            {
                if (dataBaseNotes.IsDatabaseNoteEmpty())
                {
                    model.Text = "No notes. \n Please add notes.";
                    date.Text = "";
                    km.Text = "";
                    note.Text = "";
                    back.Visibility = ViewStates.Invisible;
                    forward.Visibility = ViewStates.Invisible;
                    change.Visibility = ViewStates.Invisible;
                    add.Visibility = ViewStates.Invisible;
                    viewAll.Visibility = ViewStates.Invisible;
                    delete.Visibility = ViewStates.Invisible;
                    delete.Visibility = ViewStates.Invisible;

                    var intent = new Intent(this, typeof(Add_More_Info));
                    this.StartActivity(intent);
                }
                else ShowCurrentNote(i);
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
        public void ShowCurrentNote(int id)
        {
            
            var table = cars.GetTable();
            var cars1 = table.ToList();
            var car = cars1[Choose_Car.GetId()];
            //List<Note> notes = car.GetNotes().ToList();
            
            var allNotes = dataBaseNotes.GetTable().ToList();
            var allnote = allNotes[Choose_Car.GetId()];
            var notes = allnote.GetNotes().ToList();

            model.Text = car.Model + " " + car.Year;
            date.Text = notes[i].Date;
            km.Text = notes[i].Km+" km";
            note.Text = notes[i].Text;

        }

        public static int GetNoteId()
        {
            return i;
        }
    }
}