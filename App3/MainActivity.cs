using System;
using System.Collections.Generic;
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
using Plugin.LocalNotification;

namespace App3
{
    [Activity(Label = "Vehicle service history",Icon = "@mipmap/wrenchicon", Theme = "@style/AppTheme.NoActionBar", MainLauncher = true)]
    public class MainActivity : AppCompatActivity, NavigationView.IOnNavigationItemSelectedListener
    {
        TextView car, engine, km, kmOilChange, dateLastOilChange, currentTyres, text1, text2, text3, text4, text5;
        Button button, delete, show;
        Database cars = new Database();
        DataBaseNotes dataBaseNotes = new DataBaseNotes();
        
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);

            SetContentView(Resource.Layout.activity_main);
            AndroidX.AppCompat.Widget.Toolbar toolbar = FindViewById<AndroidX.AppCompat.Widget.Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);

            button = FindViewById<Android.Widget.Button>(Resource.Id.update);
            button.Click += onClick;

            delete = FindViewById<Android.Widget.Button>(Resource.Id.delete);
            delete.Click += onDelete;

            show = FindViewById<Android.Widget.Button>(Resource.Id.shownote);
            show.Click += Show;

            DrawerLayout drawer = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
            ActionBarDrawerToggle toggle = new ActionBarDrawerToggle(this, drawer, toolbar, Resource.String.navigation_drawer_open, Resource.String.navigation_drawer_close);
            drawer.AddDrawerListener(toggle);
            toggle.SyncState();

            NavigationView navigationView = FindViewById<NavigationView>(Resource.Id.nav_view);
            navigationView.SetNavigationItemSelectedListener(this);

            car = FindViewById<TextView>(Resource.Id.modelAndYear);
            engine = FindViewById<TextView>(Resource.Id.engine);
            km = FindViewById<TextView>(Resource.Id.km);
            kmOilChange = FindViewById<TextView>(Resource.Id.oilKm);
            dateLastOilChange = FindViewById<TextView>(Resource.Id.date);
            currentTyres = FindViewById<TextView>(Resource.Id.tyres);
            text1 = FindViewById<TextView>(Resource.Id.textView1);
            text2 = FindViewById<TextView>(Resource.Id.textView2);
            text3 = FindViewById<TextView>(Resource.Id.textView3);
            text4 = FindViewById<TextView>(Resource.Id.textView4);
            text5 = FindViewById<TextView>(Resource.Id.textView5);

            NotificationCenter.CreateNotificationChannel();
        }

        protected override void OnStart()
        {
            base.OnStart();
            var car = cars.GetTable().ToList();
            ChangingView(Choose_Car.GetId());
        }

        protected override void OnStop()
        {
            base.OnStop();
 
            var car = cars.GetTable().ToList();
            foreach (var item in car)
            {
                NotificationOil(item.DateOfOil, item.Km, item.KmOfOil, item.Model + " " + item.Year, item.Id);
                NotificationTyre(item.Tyre, item.Model + " " + item.Year, item.Id);
            }
        }
        
        public override void OnBackPressed()
        {
            DrawerLayout drawer = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
            if(drawer.IsDrawerOpen(GravityCompat.Start))
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

        private async void onClick(Object sender, EventArgs e)
        {
            var intent = new Intent(this, typeof(Update_Car));

            this.StartActivity(intent);
        }

        private async void onDelete(Object sender, EventArgs e)
        {
            var cars1 = cars.GetTable().ToList();
            var car = cars1[Choose_Car.GetId()];
            var allNotes = dataBaseNotes.GetTable().ToList();
            dataBaseNotes.Delete(allNotes[Choose_Car.GetId()]);
            cars.Delete(cars1[Choose_Car.GetId()]);

            var intent = new Intent(this, typeof(MainActivity));
            this.StartActivity(intent);
        }

        private async void Show(Object sender, EventArgs e)
        {
            var intent = new Intent(this, typeof(Show_More_Info));
            this.StartActivity(intent);
        }

        public void ChangingView(int id)
        {
            if (cars.IsDatabaseEmpty())
            {
                var intent = new Intent(this, typeof(Add_Car));

                this.StartActivity(intent);

                car.Text = "There is no car. Please add car.";
                engine.Text = "";
                km.Text = "";
                kmOilChange.Text = "";
                dateLastOilChange.Text = "";
                currentTyres.Text = "";
                text1.Text = "";
                text2.Text = "";
                text3.Text = "";
                text4.Text = "";
                text5.Text = "";
                button.Visibility = ViewStates.Invisible;
                delete.Visibility = ViewStates.Invisible;
                show.Visibility = ViewStates.Invisible;
            }
            else
            {
                var table = cars.GetTable();
                var cars2 = table.ToList();
                var car1 = cars2[id];
 
                car.Text = car1.Model+" "+car1.Year;
                engine.Text = car1.Engine;
                currentTyres.Text = car1.Tyre;
                km.Text = car1.Km;
                kmOilChange.Text = car1.KmOfOil;
                dateLastOilChange.Text = car1.DateOfOil;

                text1.Text = "Engine type";
                text2.Text = "Kilometers driven";
                text3.Text = "Last oil change kilometers";
                text4.Text = "Date of last oil change";
                text5.Text = "Current tyres";
                button.Visibility = ViewStates.Visible;
                delete.Visibility = ViewStates.Visible;
                show.Visibility = ViewStates.Visible;
            }
        }

        public void NotificationOil(string date, string km, string kmOil, string carName, int carId)
        {
            DateTime dateTime = Convert.ToDateTime(date);
            int Km = Convert.ToInt32(km);
            int KmOil = Convert.ToInt32(kmOil);
            var notification = new NotificationRequest();
            if (Km > KmOil + 5000)
            {
                notification = new NotificationRequest
                {
                    BadgeNumber = 1,
                    Title = "Old oil on "+ carName,
                    Description = "Please change oil",
                    NotificationId = 1337+carId,
                    Schedule =
                    {
                        NotifyTime = DateTime.Now.AddSeconds(5)
                    }
                };
                NotificationCenter.Current.Show(notification);
            }
            else if (DateTime.Today.Date > dateTime.Date.AddMonths(9))
            {
                notification = new NotificationRequest
                {
                    BadgeNumber = 1,
                    Title = "Old oil on " + carName,
                    Description = "Please change oil",
                    NotificationId = 1337+carId,
                    Schedule =
                    {
                        NotifyTime = DateTime.Now.AddSeconds(5)
                    }
                };
                NotificationCenter.Current.Show(notification);
            } 
        }

        public void NotificationTyre(string tyre, string carName, int carId)
        {
            DateTime date1 = new DateTime(2000, 3, 1);
            DateTime date2 = new DateTime(2000, 11, 15);
            var notification = new NotificationRequest();

            if (tyre.ToLower()!="all season")
            {
                if (DateTime.Today.Date.DayOfYear == date1.Date.DayOfYear)
                {
                    notification = new NotificationRequest
                    {
                        BadgeNumber = 1,
                        Title = "Old tyres on "+carName,
                        Description = "Please change tyres to summer tyres",
                        NotificationId = 1337+carId,
                        Schedule =
                        {
                            NotifyTime = DateTime.Now.AddSeconds(5)
                        }
                    };
                    NotificationCenter.Current.Show(notification);
                }
                else if (DateTime.Today.Date.DayOfYear == date2.Date.DayOfYear)
                {
                    notification = new NotificationRequest
                    {
                        BadgeNumber = 1,
                        Title = "Old tyres on "+ carName,
                        Description = "Please change tyres to winter tyres",
                        NotificationId = 1337+carId,
                        Schedule =
                        {
                            NotifyTime = DateTime.Now.AddSeconds(5)
                        }
                    };
                    NotificationCenter.Current.Show(notification);
                }
            }
        }
    }
}

