using System;
using System.IO;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AndroidX.AppCompat.App;
using AndroidX.AppCompat.Widget;
using AndroidX.Core.View;
using AndroidX.DrawerLayout.Widget;
using Google.Android.Material.FloatingActionButton;
using Google.Android.Material.Navigation;
using Google.Android.Material.Snackbar;

namespace App3
{
    [Activity(Label = "Add More Info", Theme = "@style/AppTheme.NoActionBar")]
    public class Add_More_Info : AppCompatActivity, NavigationView.IOnNavigationItemSelectedListener
    {
        Button save;
        DateTime date;
        DatePicker datePicker;
        EditText getKmOfNotes, getNotes;
       
        string kmOfNotes, notes;
        DataBaseNotes dataBaseNotes = new DataBaseNotes();
        Database database = new Database();

        protected override void OnCreate(Bundle savedInstanceState)
        {

            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);

            SetContentView(Resource.Layout.add_more_info);
            AndroidX.AppCompat.Widget.Toolbar toolbar = FindViewById<AndroidX.AppCompat.Widget.Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);

            save = FindViewById<Button>(Resource.Id.save_notes);
            save.Click += onSaveInfo;

            DrawerLayout drawer = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
            ActionBarDrawerToggle toggle = new ActionBarDrawerToggle(this, drawer, toolbar, Resource.String.navigation_drawer_open, Resource.String.navigation_drawer_close);
            drawer.AddDrawerListener(toggle);
            toggle.SyncState();

            NavigationView navigationView = FindViewById<NavigationView>(Resource.Id.nav_view);
            navigationView.SetNavigationItemSelectedListener(this);

            datePicker = FindViewById<DatePicker>(Resource.Id.datePicker1);
            getKmOfNotes = FindViewById<EditText>(Resource.Id.notes_km);
            getNotes = FindViewById<EditText>(Resource.Id.notes);

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

        private async void onSaveInfo(Object sender, EventArgs e)
        {
            Note note = new Note();

            var allNotes = dataBaseNotes.GetTable().ToList();
            var allnote = allNotes[Choose_Car.GetId()];

            var cars = database.GetTable().ToList();
            var car = cars[Choose_Car.GetId()];

            date = datePicker.DateTime;
            kmOfNotes = getKmOfNotes.Text;
            notes = getNotes.Text;
            
            if (Convert.ToString(date) == "" || kmOfNotes == "" || notes == "")
            {
                Toast.MakeText(this, "No info. Please enter info", ToastLength.Long).Show();
            }
            else if(date.Year < Convert.ToInt32(car.Year))
            {
                Toast.MakeText(this, " Invalid date.\n Cannot enter date of \n note older then the car.\n Please enter valid date", ToastLength.Long).Show();
            }
            else if (Convert.ToInt32(kmOfNotes) > Convert.ToInt32(car.Km))
            {
                Toast.MakeText(this, "Invalid kilometers. Please enter valid kilometers", ToastLength.Long).Show();
            }
            else
            {
                var temp = Convert.ToString(date);
                var date1 = temp.Split(' ');
                note.Date = date1[0];
                note.Km = kmOfNotes;
                note.Text = notes;

               
                allnote.InsertNote(note);
                dataBaseNotes.Update(allnote);
                Toast.MakeText(this, "Note saved", ToastLength.Short).Show();
                var intent = new Intent(this, typeof(Show_More_Info));
                this.StartActivity(intent);
            }
        }
    }
}

