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
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace App3
{
    [Activity(Label = "Add Car", Theme = "@style/AppTheme.NoActionBar")]
    public class Add_Car : Activity
    {
        private EditText getCarModel, getEngineType, getCurrentTyres,getYearOfMake, getKmDriven, getKmLastOilChange;
        private DatePicker getDateLastOilChange;
        private static string carModel, engineType, currentTyres, yearOfMake, kmDriven, kmLastOilChange, dateLastOilChange;
        Button button, cancel;
        Database cars = new Database();
        DataBaseNotes dataBaseNotes = new DataBaseNotes();
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.add_car);
       
            button = FindViewById<Button>(Resource.Id.button1);
            button.Click += onClick;
           
            cancel = FindViewById<Button>(Resource.Id.cancel);
            cancel.Click += onCancel;

            getCarModel = FindViewById<EditText>(Resource.Id.carModel);
            getEngineType = FindViewById<EditText>(Resource.Id.engineType);
            getCurrentTyres = FindViewById<EditText>(Resource.Id.tyreType);
            getYearOfMake = FindViewById<EditText>(Resource.Id.yearOfMake);
            getKmDriven = FindViewById<EditText>(Resource.Id.kilometersDriven);
            getKmLastOilChange = FindViewById<EditText>(Resource.Id.lastOilChange);
            getDateLastOilChange = FindViewById<DatePicker>(Resource.Id.dateOil);
        }

        private void onClick(Object sender, EventArgs e)
        {
            carModel = getCarModel.Text;
            engineType = getEngineType.Text;
            currentTyres = getCurrentTyres.Text;
            yearOfMake = getYearOfMake.Text;
            kmDriven = getKmDriven.Text;
            kmLastOilChange = getKmLastOilChange.Text;
            var temp = getDateLastOilChange.DateTime.Date.ToString();
            var temp1 = temp.Split(' ');
            dateLastOilChange = temp1[0];

            
            if (carModel == "" && engineType == "" && currentTyres == "" && yearOfMake == "" && kmDriven == "" && kmLastOilChange == "" && dateLastOilChange == "")
            {
                Toast.MakeText(this, "No data entered. Please enter data.", ToastLength.Long).Show();
            }
            else if (carModel == "" || engineType == "" || currentTyres == "" || yearOfMake == "" || kmDriven == "" || kmLastOilChange == "" || dateLastOilChange == "")
            {
                Toast.MakeText(this, "Not all data entered. Please enter data.", ToastLength.Long).Show();
            }
            else 
            {
                if (currentTyres.ToLower() != "all season" && currentTyres.ToLower() != "winter" && currentTyres.ToLower() != "summer") 
                {
                    Toast.MakeText(this, "Invalid tyres.", ToastLength.Long).Show();
                }
                else
                {
                    Car car = new Car();
                    
                    car.Model = carModel;
                    car.Year = yearOfMake;
                    car.Engine = engineType;
                    car.Tyre = currentTyres;
                    car.Km = kmDriven;
                    car.KmOfOil = kmLastOilChange;
                    car.DateOfOil = dateLastOilChange;
                    cars.Insert(car);
                    AllNotes allNotes = new AllNotes();
                    dataBaseNotes.Insert(allNotes);
                    Toast.MakeText(this, "Comfired data", ToastLength.Short).Show();
                    var intent = new Intent(this, typeof(MainActivity));
                    this.StartActivity(intent);
                }
            }
           
        }
        private async void onCancel(Object sender, EventArgs e)
        {
            var intent = new Intent(this, typeof(MainActivity));
            this.StartActivity(intent);
        }
        public static string GetName()
        {
            return carModel + " " + yearOfMake;
        }
    }
}